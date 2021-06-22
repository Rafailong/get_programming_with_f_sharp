module Models

open System

type Customer = { Name: string }

type Account =
    { Id: Guid
      Owner: Customer
      Balance: decimal }

type Error =
    | InvalidAmountToDeposit of amount: decimal * account: Account
    | NotEnoughFunds of requestedAmount: decimal * account: Account

type Success =
    | Withdrawal of amount: decimal * account: Account
    | Deposit of amount: decimal * account: Account
