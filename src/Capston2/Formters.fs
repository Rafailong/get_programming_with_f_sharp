module Formaters

open Models

type Formater = Result<Success, Error> -> string

let private formatError err =
    match err with
    | InvalidAmountToDeposit (amount, account) -> sprintf "Account %A: Invalid Ammount to deposit: %A" account.Id amount
    | NotEnoughFunds (requestedAmount, account) ->
        sprintf
            "Account %A: Not Enough Funds to withdraw from. Available Funds = %A - Requested Amount = %A"
            account.Id
            account.Balance
            requestedAmount

let private formatSuccess succ =
    match succ with
    | Withdrawal (amount, account) ->
        sprintf "Account %A: Performed operation 'withdraw' for %A. Balance is now %A" account.Id amount account.Balance
    | Deposit (amount, account) ->
        sprintf "Account %A: Performed operation 'deposit' for %A. Balance is now %A" account.Id amount account.Balance

let defaultFormater : Formater =
    fun result ->
        match result with
        | Error err -> formatError err
        | Ok success -> formatSuccess success
