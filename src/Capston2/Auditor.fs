module Auditor

open System.IO

open Formaters
open Models

let fileSystemAudit path =
    let auditor (formater: Formater) account msg =
        let accountLogFilePath =
            Path.Combine(path, account.Id.ToString())

        File.AppendAllText(accountLogFilePath, msg)

    if Directory.Exists path then
        auditor
    else
        Directory.CreateDirectory path |> ignore
        auditor

let consoleAudit msg = printfn "%A" msg
