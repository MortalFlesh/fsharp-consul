// Learn more about F# at http://fsharp.org

open System
open Example.Main

[<EntryPoint>]
let main argv =
    let consulUrl = "{URL}"
    let key = "{KEY}"
    let value = key |> readValue consulUrl
    printfn "Key: %s | Value: %A\n" key value

    0 // return an integer exit code
