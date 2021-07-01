// loops
for n in [ 1 .. 10 ] do
    printfn "Number is %d" n

// comprehensions
//array
[| for c in "" -> c |] |> ignore
// list
[ for c in "" -> c ] |> ignore
// seq
seq { for c in "" -> c } |> ignore

// if/then is an expression
let r = if true then 1 else 3

// pattern matching (is also an expression)
// returning unit in this example
let cmd = 'a'

match cmd with
| 'd'
| 'D' -> printfn "deposit"
| 'w'
| 'W' -> printfn "withdraw"
| 'x'
| 'X' -> printfn "exiting program"
| z -> printfn "invalid command %A " z


// now you try - 1
// it works top-down: most specific to most general
let getCreditLimit customer =
    match customer with
    // | _ -> 250 // uncomment to produce a warning regading unreachable cases
    | "medium", 1 -> 500
    // | "good", 0
    // | "good", 1 -> 750
    | "good", years when years < 2 -> 750 // using guards
    | "good", 2 -> 1000
    | "good", _ -> 2000
    | _ -> 250 // comment-out to produce a warning regading non-exhaustive pattern

getCreditLimit ("medium", 1) |> ignore
// getCreditLimit ("bad", 1) |> ignore // will raise MatchFailureException


type Customer = { Balance: int; Name: string }

// matching on list
let handleCustomer (customers: Customer list) =
    match customers with
    | [] -> failwith "No customer supplied!"
    // matching on records (desconstructing records just as we do with tuples)
    // you don't have to fill in all the fields
    | [ { Name = name } ] -> printfn "Single customer, name is %s" name
    | [ { Balance = b1 }; { Balance = b2 } ] -> printfn "Two customers, balance = %d" (b1 + b2)
    | css -> printfn "Customers supplied: %d" css.Length

// handleCustomer [] // throws exception
handleCustomer [ { Balance = 10; Name = "Joe" } ] // prints name
