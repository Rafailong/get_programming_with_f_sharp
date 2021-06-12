module Unit1
// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System

type Destination =
    | Home
    | Office
    | Stadium
    | Gas_Station

let requiredPetroUnits d =
    match d with
    | Home -> 25
    | Office -> 50
    | Stadium -> 25
    | Gas_Station -> 10

let parseDestiny str =
    match str with
    | "home" -> Some Home
    | "office" -> Some Office
    | "stadium" -> Some Stadium
    | "gas" -> Some Gas_Station
    | _ -> None

let travel petrol origin destiny =
    match (origin, destiny) with
    | (Gas_Station, Gas_Station) -> Some(petrol + 50)
    | (o, d) when o = d -> Some petrol
    | (_, d) ->
        let requiredPetrol = requiredPetroUnits d

        if petrol < requiredPetrol then
            None
        else
            printfn "Traveling to %A" destiny
            Some(petrol - requiredPetrol)

let rec program petrol origin =
    printfn "Stats: Petrol: %d - Location: %A" petrol origin
    printfn "Enter you destination: home, office, stadium, gas"

    let input = Console.ReadLine()
    let maybeDestiny = parseDestiny (input.Trim())

    match maybeDestiny with
    | Some destiny ->
        let somePetrol = travel petrol origin destiny

        match somePetrol with
        | Some left -> program left destiny
        | None -> printfn "Not enough petrol to travel."
    | None -> printfn "Going nowhere. Invalid destination."

[<EntryPoint>]
let main argv =
    program 100 Home
    printfn "Program terminated."
    0
