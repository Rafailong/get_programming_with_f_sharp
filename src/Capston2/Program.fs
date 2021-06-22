open System

open Operations
open Models

[<EntryPoint>]
let main argv =

    printfn "Enter your name!"
    let customerName = Console.ReadLine()

    let customer = { Customer.Name = customerName }

    let mutable account =
        { Account.Id = Guid.NewGuid()
          Owner = customer
          Balance = 0m }

    printfn "Your account with Balance $0.0 has been created!"

    let mutable operation = ""

    while (operation <> "x") do
        printfn
            """
            Available operations are:
            - Withdraw (w)
            - Deposit (d)
            - Exit (x)
            """

        operation <- Console.ReadLine()

        match operation with
        | "w" ->
            printfn "Enter amount to withdraw."
            let updatedAccount = withdrawWithConsoleaudit (Console.ReadLine() |> Decimal.Parse) account
            account <- updatedAccount
        | "d" ->
            printfn "Enter amount to deposit."
            let updatedAccount = depositWithConsoleaudit (Console.ReadLine() |> Decimal.Parse) account
            account <- updatedAccount
        | "x" -> printfn "Have a nice day!"
        | _ -> printfn "Invalid option."

    0 // return an integer exit code
