module Operations

open Models

let deposit amount account =
    if amount < 0m then
        Error <| InvalidAmountToDeposit(amount, account)
    else
        let updated =
            { account with
                  Account.Balance = account.Balance + amount }

        Ok <| Deposit(amount, updated)

let withdraw amount account =
    if amount > account.Balance then
        Error <| NotEnoughFunds(amount, account)
    else
        let updated =
            { account with
                  Account.Balance = account.Balance - amount }

        Ok <| Withdrawal(amount, updated)

let auditAs
    (formater: Result<Success, Error> -> string)
    (auditor: string -> unit)
    (operation: decimal -> Account -> Result<Success, Error>)
    (amount: decimal)
    (account: Account)
    =
    let result = operation amount account
    let msg = formater result
    auditor msg |> ignore

    match result with
    | Ok success ->
        match success with
        | Deposit (_, updatedAccount) -> updatedAccount
        | Withdrawal (_, updatedAccount) -> updatedAccount
    | Error _ -> account

let withdrawWithConsoleaudit =
    auditAs Formaters.defaultFormater Auditor.consoleAudit withdraw

let depositWithConsoleaudit =
    auditAs Formaters.defaultFormater Auditor.consoleAudit deposit
