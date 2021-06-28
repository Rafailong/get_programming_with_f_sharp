module Auditor

open System
open System.IO

open Formaters
open Models

let fileSystemAudit path =
    let auditor (msg: string) =
        let accountLogFilePath =
            Path.Combine(path, string DateTime.Now.Ticks)

        using (File.CreateText(accountLogFilePath)) (fun streamWriter -> streamWriter.Write(msg))


    if Directory.Exists path then
        auditor
    else
        Directory.CreateDirectory path |> ignore
        auditor

let consoleAudit msg = printfn "%A" msg
