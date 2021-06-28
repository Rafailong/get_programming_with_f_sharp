module Models

open System
open Fleece.Newtonsoft
open Fleece.Newtonsoft.Operators

type Customer =
    { Name: string }
    static member JsonObjCodec =
        fun name -> { Customer.Name = name }
        <!> jreq "name" (fun x -> Some x.Name)
        |> Codec.ofConcrete

type Account =
    { Id: Guid
      Owner: Customer
      Balance: decimal } with
    static member JsonObjCodec =
        fun id customer balance -> { Account.Id = id; Owner = customer; Balance = balance }
        <!> jreq "id"      (fun x -> Some x.Id)
        <*> jreq "owner"   (fun x -> Some x.Owner)
        <*> jreq "balance" (fun x -> Some x.Balance)
        |> Codec.ofConcrete

type Error =
    | InvalidAmountToDeposit of amount: decimal * account: Account
    | NotEnoughFunds of requestedAmount: decimal * account: Account
    static member JsonObjCodec =
        [ InvalidAmountToDeposit
          <!> jreq
                  "invalidAmountToDeposit"
                  (function
                  | (InvalidAmountToDeposit (amt, act)) -> Some(amt, act)
                  | _ -> None)
          NotEnoughFunds
          <!> jreq
                  "notEnoughFunds"
                  (function
                  | (NotEnoughFunds (amt, act)) -> Some(amt, act)
                  | _ -> None) ]
        |> jchoice

type Success =
    | Withdrawal of amount: decimal * account: Account
    | Deposit of amount: decimal * account: Account
    static member JsonObjCodec =
        [ Withdrawal
          <!> jreq
                  "withdrawal"
                  (function
                  | (Withdrawal (amt, act)) -> Some(amt, act)
                  | _ -> None)
          Deposit
          <!> jreq
                  "deposit"
                  (function
                  | (Deposit (amt, act)) -> Some(amt, act)
                  | _ -> None) ]
        |> jchoice
