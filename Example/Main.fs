module Example.Main

open Consul
open System.Text
open System

let decode value =
    match value with
    | null -> "<null-value>"
    | value -> Encoding.UTF8.GetString(value, 0, value.Length)

let decodeResultValue (result: QueryResult<KVPair>) =
    match result.Response with
    | null -> "<null-response>"
    | response -> response.Value |> decode

let uri (url: string) =
    let uriBuilder = new UriBuilder(url);
    uriBuilder.Uri

let consulKV job (consulUrl: string) =
    use client = new ConsulClient()
    client.Config.Address <- consulUrl |> uri
    
    job client.KV

let readValue (consulUrl: string) (key: string) =
    use client = new ConsulClient()
    client.Config.Address <- consulUrl |> uri

    printfn "Get value for \"%s\" ..." key
    let pair = client.KV.Get (key)

    pair.Result
    |> decodeResultValue
