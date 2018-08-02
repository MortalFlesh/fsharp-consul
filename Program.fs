// Learn more about F# at http://fsharp.org

open System
open System.Text
open Consul

let uri (url: string) =
    let uriBuilder = new UriBuilder(url);
    uriBuilder.Uri

let decode value =
    match value with
    | null -> "<null-value>"
    | value -> Encoding.UTF8.GetString(value, 0, value.Length)

let decodeResultValue (result: QueryResult<KVPair>) =
    match result.Response with
    | null -> "<null-response>"
    | response -> response.Value |> decode

let readValue (consulUrl: string) (key: string) =
    use client = new ConsulClient()
    client.Config.Address <- consulUrl |> uri

    printfn "Get value for \"%s\" ..." key
    let pair = client.KV.Get (key)

    pair.Result |> decodeResultValue

[<EntryPoint>]
let main argv =
    let consulUrl = "{URL}"
    let key = "{KEY}"
    let value = key |> readValue consulUrl
    printfn "Key: %s | Value: %A\n" key value

    0 // return an integer exit code
