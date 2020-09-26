module FsHttp.DslCE

open Dsl
open Domain


type HttpBuilderBase() =
    class
    end


[<AutoOpen>]
module R =
    type HttpBuilderBase with

        [<CustomOperation("Request")>]
        member this.Request(StartingContext, method, url) = request method url

        // RFC 2626 specifies 8 methods
        [<CustomOperation("GET")>]
        member this.Get(StartingContext, url) = get url

        [<CustomOperation("PUT")>]
        member this.Put(StartingContext, url) = put url

        [<CustomOperation("POST")>]
        member this.Post(StartingContext, url) = post url

        [<CustomOperation("DELETE")>]
        member this.Delete(StartingContext, url) = delete url

        [<CustomOperation("OPTIONS")>]
        member this.Options(StartingContext, url) = options url

        [<CustomOperation("HEAD")>]
        member this.Head(StartingContext, url) = head url

        [<CustomOperation("TRACE")>]
        member this.Trace(StartingContext, url) = trace url

        [<CustomOperation("CONNECT")>]
        member this.Connect(StartingContext, url) = connect url

        [<CustomOperation("PATCH")>]
        member this.Patch(StartingContext, url) = patch url

// RFC 4918 (WebDAV) adds 7 methods
// TODO


[<AutoOpen>]
module H =
    type HttpBuilderBase with

        /// Content-Types that are acceptable for the response
        [<CustomOperation("Accept")>]
        member this.Accept(context, contentType) = Dsl.H.accept contentType context

        /// Character sets that are acceptable
        [<CustomOperation("AcceptCharset")>]
        member this.AcceptCharset(context, characterSets) = Dsl.H.acceptCharset characterSets context

        /// Acceptable version in time
        [<CustomOperation("AcceptDatetime")>]
        member this.AcceptDatetime(context, dateTime) = Dsl.H.acceptDatetime dateTime context

        /// List of acceptable encodings. See HTTP compression.
        [<CustomOperation("AcceptEncoding")>]
        member this.AcceptEncoding(context, encoding) = Dsl.H.acceptEncoding encoding context

        /// List of acceptable human languages for response
        [<CustomOperation("AcceptLanguage")>]
        member this.AcceptLanguage(context, language) = Dsl.H.acceptLanguage language context

        /// Append query params
        [<CustomOperation("Query")>]
        member this.Query(context, queryParams) = Dsl.H.query context queryParams id
        
        /// Authentication credentials for HTTP authentication
        [<CustomOperation("Authorization")>]
        member this.Authorization(context, credentials) = Dsl.H.authorization credentials context

        /// Authentication header using Bearer Auth token
        [<CustomOperation("BearerAuth")>]
        member this.BearerAuth(context, token) = Dsl.H.bearerAuth token context

        /// Authentication header using Basic Auth encoding
        [<CustomOperation("BasicAuth")>]
        member this.BasicAuth(context, username, password) = Dsl.H.basicAuth username password context

        /// Used to specify directives that MUST be obeyed by all caching mechanisms along the request/response chain
        [<CustomOperation("CacheControl")>]
        member this.CacheControl(context, control) = Dsl.H.cacheControl control context

        /// What type of connection the user-agent would prefer
        [<CustomOperation("Connection")>]
        member this.Connection(context, connection) = Dsl.H.connection connection context

        /// An HTTP cookie previously sent by the server with 'Set-Cookie'.
        [<CustomOperation("Cookie")>]
        member this.SetCookie(context, name, value) = Dsl.H.cookie name value context

        /// An HTTP cookie previously sent by the server with 'Set-Cookie' with
        /// the subset of URIs on the origin server to which this Cookie applies.
        [<CustomOperation("CookieForPath")>]
        member this.SetCookieForPath(context, name, value, path) = Dsl.H.cookieForPath name value path context

        /// An HTTP cookie previously sent by the server with 'Set-Cookie' with
        /// the subset of URIs on the origin server to which this Cookie applies
        /// and the internet domain for which this Cookie is valid.
        [<CustomOperation("CookieForDomain")>]
        member this.SetCookieForDomain(context, name, value, path, domain) =
            Dsl.H.cookieForDomain name value path domain context

        /// The date and time that the message was sent
        [<CustomOperation("Date")>]
        member this.Date(context, date) = Dsl.H.date date context

        /// Indicates that particular server behaviors are required by the client
        [<CustomOperation("Expect")>]
        member this.Expect(context, behaviors) = Dsl.H.expect behaviors context

        /// Gives the date/time after which the response is considered stale
        [<CustomOperation("Expires")>]
        member this.Expires(context, dateTime) = Dsl.H.expires dateTime context

        /// The email address of the user making the request
        [<CustomOperation("From")>]
        member this.From(context, email) = Dsl.H.from email context

        /// Custom header
        [<CustomOperation("Header")>]
        member this.Header(context, key, value) = Dsl.H.header key value context

        /// The domain name of the server (for virtual hosting), and the TCP port number on which the server is listening.
        /// The port number may be omitted if the port is the standard port for the service requested.
        [<CustomOperation("Host")>]
        member this.Host(context, host) = Dsl.H.host host context

        /// Only perform the action if the client supplied entity matches the same entity on the server.
        /// This is mainly for methods like PUT to only update a resource if it has not been modified since the user last updated it. If-Match: "737060cd8c284d8af7ad3082f209582d" Permanent
        [<CustomOperation("IfMatch")>]
        member this.IfMatch(context, entity) = Dsl.H.ifMatch entity context

        /// Allows a 304 Not Modified to be returned if content is unchanged
        [<CustomOperation("IfModifiedSince")>]
        member this.IfModifiedSince(context, dateTime) = Dsl.H.ifModifiedSince dateTime context

        /// Allows a 304 Not Modified to be returned if content is unchanged
        [<CustomOperation("IfNoneMatch")>]
        member this.IfNoneMatch(context, etag) = Dsl.H.ifNoneMatch etag context

        /// If the entity is unchanged, send me the part(s) that I am missing; otherwise, send me the entire new entity
        [<CustomOperation("IfRange")>]
        member this.IfRange(context, range) = Dsl.H.ifRange range context

        /// Only send the response if the entity has not been modified since a specific time
        [<CustomOperation("IfUnmodifiedSince")>]
        member this.IfUnmodifiedSince(context, dateTime) = Dsl.H.ifUnmodifiedSince dateTime context

        /// Specifies a parameter used into order to maintain a persistent connection
        [<CustomOperation("KeepAlive")>]
        member this.KeepAlive(context, keepAlive) = Dsl.H.keepAlive keepAlive context

        /// Specifies the date and time at which the accompanying body data was last modified
        [<CustomOperation("LastModified")>]
        member this.LastModified(context, dateTime) = Dsl.H.lastModified dateTime context

        /// Limit the number of times the message can be forwarded through proxies or gateways
        [<CustomOperation("MaxForwards")>]
        member this.MaxForwards(context, count) = Dsl.H.maxForwards count context

        /// Initiates a request for cross-origin resource sharing (asks server for an 'Access-Control-Allow-Origin' response header)
        [<CustomOperation("Origin")>]
        member this.Origin(context, origin) = Dsl.H.origin origin context

        /// Implementation-specific headers that may have various effects anywhere along the request-response chain.
        [<CustomOperation("Pragma")>]
        member this.Pragma(context, pragma) = Dsl.H.pragma pragma context

        /// Optional instructions to the server to control request processing. See RFC https://tools.ietf.org/html/rfc7240 for more details
        [<CustomOperation("Prefer")>]
        member this.Prefer(context, prefer) = Dsl.H.prefer prefer context

        /// Authorization credentials for connecting to a proxy.
        [<CustomOperation("ProxyAuthorization")>]
        member this.ProxyAuthorization(context, credentials) = Dsl.H.proxyAuthorization credentials context

        /// Request only part of an entity. Bytes are numbered from 0
        [<CustomOperation("Range")>]
        member this.Range(context, start, finish) = Dsl.H.range start finish context

        /// This is the address of the previous web page from which a link to the currently requested page was followed.
        /// (The word "referrer" is misspelled in the RFC as well as in most implementations.)
        [<CustomOperation("Referer")>]
        member this.Referer(context, referer) = Dsl.H.referer referer context

        /// The transfer encodings the user agent is willing to accept: the same values as for the response header
        /// Transfer-Encoding can be used, plus the "trailers" value (related to the "chunked" transfer method) to
        /// notify the server it expects to receive additional headers (the trailers) after the last, zero-sized, chunk.
        [<CustomOperation("TE")>]
        member this.TE(context, te) = Dsl.H.te te context

        /// The Trailer general field value indicates that the given set of header fields is present in the trailer of a message encoded with chunked transfer-coding
        [<CustomOperation("Trailer")>]
        member this.Trailer(context, trailer) = Dsl.H.trailer trailer context

        /// The TransferEncoding header indicates the form of encoding used to safely transfer the entity to the user.
        /// The valid directives are one of: chunked, compress, deflate, gzip, orentity.
        [<CustomOperation("TransferEncoding")>]
        member this.TransferEncoding(context, directive) = Dsl.H.transferEncoding directive context

        /// Microsoft extension to the HTTP specification used in conjunction with WebDAV functionality.
        [<CustomOperation("Translate")>]
        member this.Translate(context, translate) = Dsl.H.translate translate context

        /// Specifies additional communications protocols that the client supports.
        [<CustomOperation("Upgrade")>]
        member this.Upgrade(context, upgrade) = Dsl.H.upgrade upgrade context

        /// The user agent string of the user agent
        [<CustomOperation("UserAgent")>]
        member this.UserAgent(context, userAgent) = Dsl.H.userAgent userAgent context

        /// Informs the server of proxies through which the request was sent
        [<CustomOperation("Via")>]
        member this.Via(context, server) = Dsl.H.via server context

        /// A general warning about possible problems with the entity body
        [<CustomOperation("Warning")>]
        member this.Warning(context, message) = Dsl.H.warning message context

        /// Override HTTP method.
        [<CustomOperation("XHTTPMethodOverride")>]
        member this.XHTTPMethodOverride(context, httpMethod) = Dsl.H.xhttpMethodOverride httpMethod context


[<AutoOpen>]
module B =
    type HttpBuilderBase with

        [<CustomOperation("body")>]
        member this.Body(context) = Dsl.B.body context

        [<CustomOperation("binary")>]
        member this.Binary(context, data) = Dsl.B.binary data context

        [<CustomOperation("stream")>]
        member this.Stream(context, stream) = Dsl.B.stream stream context

        [<CustomOperation("text")>]
        member this.Text(context, text) = Dsl.B.text text context

        [<CustomOperation("json")>]
        member this.Json(context, json) = Dsl.B.json json context

        [<CustomOperation("formUrlEncoded")>]
        member this.FormUrlEncoded(context, data) = Dsl.B.formUrlEncoded data context

        [<CustomOperation("file")>]
        member this.File(context, path) = Dsl.B.file path context

        /// The type of encoding used on the data
        [<CustomOperation("ContentEncoding")>]
        member this.ContentEncoding(context, encoding) = Dsl.B.contentEncoding encoding context

        /// The MIME type of the body of the request (used with POST and PUT requests)
        [<CustomOperation("ContentType")>]
        member this.ContentType(context, contentType) = Dsl.B.contentType contentType context

        /// The MIME type of the body of the request (used with POST and PUT requests) with an explicit encoding
        [<CustomOperation("ContentTypeWithEncoding")>]
        member this.ContentTypeWithEncoding(context, contentType, charset) =
            Dsl.B.contentTypeWithEncoding contentType charset context


[<AutoOpen>]
module M =
    type HttpBuilderBase with

        [<CustomOperation("multipart")>]
        member this.Multipart(context) = Dsl.M.multipart context

        [<CustomOperation("part")>]
        member this.Part(context, content, defaultContentType, name) =
            Dsl.M.part content defaultContentType name context

        [<CustomOperation("valuePart")>]
        member this.ValuePart(context, name, value) = Dsl.M.valuePart name value context

        [<CustomOperation("filePart")>]
        member this.FilePart(context, path) = Dsl.M.filePart path context

        [<CustomOperation("filePartWithName")>]
        member this.FilePartWithName(context, name, path) = Dsl.M.filePartWithName name path context

        /// The MIME type of the body of the request (used with POST and PUT requests)
        [<CustomOperation("ContentTypePart")>]
        member this.ContentTypePart(context, contentType) = Dsl.M.contentType contentType context


[<AutoOpen>]
module Config =
    
    open System.Net.Http
    
    type HttpBuilderBase with

        [<CustomOperation("timeout")>]
        member this.Timeout(context, value) = Dsl.Config.timeout value context

        [<CustomOperation("timeoutInSeconds")>]
        member this.TimeoutInSeconds(context, value) = Dsl.Config.timeoutInSeconds value context

        [<CustomOperation("transformHttpRequestMessage")>]
        member this.TransformHttpRequestMessage(context, map) = Dsl.Config.transformHttpRequestMessage map context

        [<CustomOperation("transformHttpClient")>]
        member this.TransformHttpClient(context, map) = Dsl.Config.transformHttpClient map context

        [<CustomOperation("proxy")>]
        member this.Proxy(context, url) = Dsl.Config.proxy url context

        [<CustomOperation("proxyWithCredentials")>]
        member this.ProxyWithCredentials(context, url, credentials) =
            Dsl.Config.proxyWithCredentials url credentials context

        /// Inject a HttpClient that will be used directly (most config parameters specified here will be ignored). 
        [<CustomOperation("useHttpClient")>]
        member this.UseHttpClient(context, client: HttpClient) =
            Dsl.Config.useHttpClient client context

[<AutoOpen>]
module Builder =

    type HttpBuilderBase with
        member this.Bind(m, f) = f m
        member this.Return(x) = x
        member this.For(m, f) = this.Bind m f

    type HttpRequestBuilder<'a>(context: 'a) =
        inherit HttpBuilderBase()
        member this.Yield(x) = context

    let httpRequest context = HttpRequestBuilder context

    // TODO: this can be a better way of chaining requests
    // (as a replacement / enhancement for HttpRequestBuilder):
    //type HttpChainableBuilder<'a>(context: 'a) =
    //    inherit HttpBuilderBase()
    //    member this.Yield(x) = context
    //    member this.Delay(f: unit -> 'a) = HttpChainableBuilder<'a>(f())
    //let httpChain context = HttpContextBuilder context

    type HttpStartingBuilder() =
        inherit HttpRequestBuilder<StartingContext>(StartingContext)

    type HttpBuilderSync() =
        inherit HttpStartingBuilder()
        member inline this.Delay(f: unit -> 'a) = f() |> Request.send

    let http = HttpBuilderSync()

    type HttpBuilderAsync() =
        inherit HttpStartingBuilder()
        member inline this.Delay(f: unit -> 'a) = f() |> Request.sendAsync

    let httpAsync = HttpBuilderAsync()

    type HttpBuilderLazy() =
        inherit HttpStartingBuilder()

    let httpLazy = HttpBuilderLazy()

    type HttpMessageBuilder() =
        inherit HttpStartingBuilder()
        member inline this.Delay(f: unit -> IContext) =
            f()
            |> fun context -> context.ToRequest()
            |> Request.toMessage

    let httpMsg = HttpMessageBuilder()


[<AutoOpen>]
module Shortcuts =

    type httpShortcutBuilder(context) =
        inherit HttpRequestBuilder<HeaderContext>(context)
        member inline this.Delay(f: unit -> 'a) = f() |> Request.send

        [<CustomOperation("id")>]
        member this.Id(context) = context

    let private req = httpRequest (StartingContext)

    let request method url = req.Request(StartingContext, method, url) |> httpShortcutBuilder
    let get url = req.Get(StartingContext, url) |> httpShortcutBuilder
    let put url = req.Put(StartingContext, url) |> httpShortcutBuilder
    let post url = req.Post(StartingContext, url) |> httpShortcutBuilder
    let delete url = req.Delete(StartingContext, url) |> httpShortcutBuilder
    let options url = req.Options(StartingContext, url) |> httpShortcutBuilder
    let head url = req.Head(StartingContext, url) |> httpShortcutBuilder
    let trace url = req.Trace(StartingContext, url) |> httpShortcutBuilder
    let connect url = req.Connect(StartingContext, url) |> httpShortcutBuilder
    let patch url = req.Patch(StartingContext, url) |> httpShortcutBuilder

[<AutoOpen>]
module Fsi =

    open FsHttp.Fsi

    let raw = rawPrinterTransformer |> modifyPrinter
    let headerOnly = headerOnlyPrinterTransformer |> modifyPrinter
    let show maxLength = showPrinterTransformer maxLength |> modifyPrinter
    let preview = previewPrinterTransformer |> modifyPrinter
    let prv = preview
    let go = preview
    let expand = expandPrinterTransformer |> modifyPrinter
    let exp = expand

    let inline private modifyPrintHint f (context: ^t) =
        let transformPrintHint (config: Config) = { config with printHint = f config.printHint }
        (^t: (member Configure: (Config -> Config) -> ^t) (context, transformPrintHint))

    type HttpBuilderBase with

        [<CustomOperation("raw")>]
        member inline this.Raw(context: ^t) = modifyPrintHint rawPrinterTransformer context

        [<CustomOperation("headerOnly")>]
        member inline this.HeaderOnly(context: ^t) = modifyPrintHint headerOnlyPrinterTransformer context

        [<CustomOperation("show")>]
        member inline this.Show(context: ^t, maxLength) = modifyPrintHint (showPrinterTransformer maxLength) context

        [<CustomOperation("preview")>]
        member inline this.Preview(context: ^t) = modifyPrintHint previewPrinterTransformer context

        [<CustomOperation("prv")>]
        member inline this.Prv(context: ^t) = modifyPrintHint previewPrinterTransformer context

        [<CustomOperation("go")>]
        member inline this.Go(context: ^t) = modifyPrintHint previewPrinterTransformer context

        [<CustomOperation("expand")>]
        member inline this.Expand(context: ^t) = modifyPrintHint expandPrinterTransformer context

        [<CustomOperation("exp")>]
        member inline this.Exp(context: ^t) = modifyPrintHint expandPrinterTransformer context
