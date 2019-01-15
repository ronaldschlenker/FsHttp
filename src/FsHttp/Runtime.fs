
namespace FsHttp

open System
open System.Linq
open System.Net.Http
open System.Text
open FsHttp
open FSharp.Data

[<AutoOpen>]
module Runtime =

    type PrintHint =
        | Show of maxLength: int
        | Expand

    type Response = {
        content: HttpContent;
        headers: Headers.HttpResponseHeaders;
        reasonPhrase: string;
        statusCode: System.Net.HttpStatusCode;
        requestMessage: HttpRequestMessage;
        version: Version;
        printHint: PrintHint
    }

    let previewPrintLength = 100

    let inline finalizeContext (context: ^t) =
        (^t: (static member Finalize: ^t -> FinalContext) (context))

    let inline sendAsync (context: ^t) =
    
        let finalContext = finalizeContext context
        let invoke() =
            let request = finalContext.request
            let requestMessage = new HttpRequestMessage(request.method, request.url)
        
            requestMessage.Content <-
                match finalContext.content with
                | Some c -> 
                    let stringContent = new StringContent(c.content, Encoding.UTF8, c.contentType)
                    for name,value in c.headers do
                        stringContent.Headers.TryAddWithoutValidation(name, value) |> ignore
                    stringContent
                | _ -> null
        
            for name,value in request.headers do
                requestMessage.Headers.TryAddWithoutValidation(name, value) |> ignore

            // TODO: dispose        
            let client = new HttpClient()
            client.SendAsync(requestMessage)

        async {
            let! response = invoke() |> Async.AwaitTask
            return
                {
                    content = response.Content;
                    headers = response.Headers;
                    reasonPhrase = response.ReasonPhrase;
                    statusCode = response.StatusCode;
                    requestMessage = response.RequestMessage;
                    version = response.Version;
                    printHint = Show previewPrintLength
                }
        }

    let inline send (context: ^t) =
        context |> sendAsync |> Async.RunSynchronously

    let headerToString (r: Response) =
        let sb = StringBuilder()
        let printHeader (headers: Headers.HttpHeaders) =
            // TODO: Table formatting
            for h in headers do
                let values = String.Join(", ", h.Value)
                sb.AppendLine (sprintf "%s: %s" h.Key values) |> ignore
        sb.AppendLine() |> ignore
        sb.AppendLine (sprintf "HTTP/%s %d %s" (r.version.ToString()) (int r.statusCode) (string r.statusCode)) |> ignore
        printHeader r.headers
        sb.AppendLine("---") |> ignore
        printHeader r.content.Headers
        sb.ToString()
    
    // TODO: Don't read the whole response; read only requested chars.
    let toStringAsync maxLength (r: Response) =
        let getTrimChars (s: string) =
            match s.Length with
            | l when l > maxLength -> "\n..."
            | _ -> ""
        async {
            let! content = r.content.ReadAsStringAsync() |> Async.AwaitTask
            return
                System.String(content.Take(maxLength).ToArray())
                + (getTrimChars content)
        }
    let toString maxLength (r: Response) =
        (toStringAsync maxLength r) |> Async.RunSynchronously

    let toText (r: Response) =  toString Int32.MaxValue r

    let toJson (r: Response) =  toText r |> JsonValue.Parse
    let toJsonArray (r: Response) =  ((toString Int32.MaxValue r) |> JsonValue.Parse).AsArray()

    let show maxLength r = { r with printHint = Show maxLength }
    let preview r = r
    let expand r = { r with printHint = Expand }

        
    type HttpBuilder with
        member this.Bind(m, f) = f m
        member this.Return(x) = x
        member this.Yield(x) = StartingContext
        member this.For(m, f) = this.Bind m f

    type HttpBuilderSync() =
        inherit HttpBuilder()
        member inline this.Delay(f: unit -> 'a) =
            f() |> send

    type HttpBuilderAsync() =
        inherit HttpBuilder()
        member inline this.Delay(f: unit -> 'a) =
            f() |> sendAsync

    type HttpBuilderLazySync() =
        inherit HttpBuilder()
        member inline this.Delay(f: unit -> 'a) =
            fun() -> f() |> finalizeContext

    let http = HttpBuilderSync()
    let httpAsync = HttpBuilderAsync()
    let httpLazy = HttpBuilderLazySync()

    // TODO:
    // Multipart
    // mime types
    // content types
    // body: text, binary, json, etc.
