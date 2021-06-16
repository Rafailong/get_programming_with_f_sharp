// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open Domain
open Operations

let isAdult = isOlderThan 18

[<EntryPoint>]
let main argv =
    let joe = {
        Customer.FirstName = "Joe"
        LastName = "Doe"
        Age = 20
    }
    let msg = sprintf "Is %s an adult? %A" joe.FirstName (isAdult joe)
    printfn "%s" msg
    0 // return an integer exit code