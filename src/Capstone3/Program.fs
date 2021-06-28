open System

open Operations
open Models

let private fileSystemRepoRootPath customerName = IO.Path.Combine(".", sprintf "%s/" customerName)

[<EntryPoint>]
let main argv =

    printfn "Enter your name!"
    let customerName = Console.ReadLine()

    let customer = { Customer.Name = customerName }

    let customerRepoRoot = fileSystemRepoRootPath customer.Name

    let openingAccount =
        match (FileRepository.openAccountFor customerRepoRoot customer) with
        | Some act -> act
        | None ->
            { Account.Owner = customer
              Balance = 0M
              Id = Guid.NewGuid() }

    let isStopCommand (command: char) = command = 'x'

    let isValidCommand (command: char) =
        match command with
        | 'd' -> true
        | 'w' -> true
        | cmd -> isStopCommand cmd

    let consoleCommands =
        seq {
            while true do
                printfn "(d)eposit, (w)ithdraw or e(x)it: "
                yield Console.ReadKey().KeyChar
        }

    let withdrawWithFileAudit =
        auditAs Formaters.defaultFormater (Auditor.fileSystemAudit customerRepoRoot) withdraw

    let depositWithFileAudit =
        auditAs Formaters.defaultFormater (Auditor.fileSystemAudit customerRepoRoot) deposit

    let processCommand (account: Account) (command: char, amount: decimal) =
        match command with
        | 'd' -> depositWithFileAudit amount account
        | 'w' -> withdrawWithFileAudit amount account
        | _ -> account

    let getAmountConsole chr =
        printfn "Enter amount:"
        chr, (Console.ReadLine() |> Decimal.Parse)

    let account =
        consoleCommands
        |> Seq.filter isValidCommand
        |> Seq.takeWhile (not << isStopCommand)
        |> Seq.map getAmountConsole
        |> Seq.fold processCommand openingAccount

    printfn "%A" account

    0 // return an integer exit code


// ravila@Mac: ~/MyDev/F_Sharp/GetProgrammingWithFSharp/src/Capstone3 git:(master) ✗ 
// ➜   dotnet run 