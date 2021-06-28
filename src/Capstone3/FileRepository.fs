module FileRepository

open System
open System.IO

open Models
open Fleece.Newtonsoft

let private loadTransaction customerDir transactionFile =
    printfn "Loading transaction: %s" transactionFile
    (File.ReadAllText
     <| Path.Combine(customerDir, transactionFile))
    |> parseJson

let private listTransactionsFrom (dirPath: string) : ParseResult<Success> seq =
    printfn "Listing transaction from %s" dirPath
    Directory.EnumerateFileSystemEntries dirPath
    |> Seq.sort
    |> Seq.map (fun str -> Array.last <| str.Split('/'))
    |> Seq.map (loadTransaction dirPath)

let private applyTransaction account result =
    match result with
    | Ok success ->
        printfn "Appliying account update with: %A" success
        match success with
        | Withdrawal (_, act) -> act
        | Deposit (_, act) -> act
    | _ -> account

let openAccountFor (customerDir: string) (customer: Customer) =
    if not <| Directory.Exists customerDir then
        None
    else
        let account =
            { Account.Owner = customer
              Id = Guid.Empty
              Balance = 0m }

        Some(
            listTransactionsFrom customerDir
            |> Seq.fold applyTransaction account
        )
