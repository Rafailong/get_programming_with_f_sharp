module Unit1

open System

type Destination =
    | Home
    | Office
    | Stadium
    | Gas_Station

type Result =
    | InvalidDestiny of invalid: string
    | NotEnoughPetrol of petrol: int * destiniy: Destination
    | StayingAtLocation of petrol: int * location: Destination
    | Travel of petrol: int * origin: Destination * destiny: Destination

    member this.PrettyPrint =
        match this with
        | InvalidDestiny (invalid) -> sprintf "Invalid destiny = %s" invalid
        | NotEnoughPetrol (petrol, destiny) -> sprintf "Not enough petrol (%d) to travel to %A" petrol destiny
        | StayingAtLocation (petrol, location) -> sprintf "staying at same location %A with %d petrol units" location petrol
        | Travel (petrol, origin, destiny) -> sprintf "Travel from %A to %A with %d petrol units" origin destiny petrol

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
    | (Gas_Station, Gas_Station) -> StayingAtLocation (petrol+50, origin)
    | (o, d) when o = d -> StayingAtLocation (petrol, origin)
    | (_, d) ->
        let requiredPetrol = requiredPetroUnits d

        if petrol < requiredPetrol then NotEnoughPetrol (petrol, destiny)
        else Travel (petrol-requiredPetrol, origin, destiny)

let rec program inputReader logEvent petrol origin =
    let input: string = inputReader()
    let maybeDestiny = parseDestiny (input.Trim())

    let partial = program inputReader logEvent

    match maybeDestiny with
    | Some destiny ->
        let result = travel petrol origin destiny
        logEvent result
        match result with
        | Travel (petrol, _, destiny) -> partial petrol destiny
        | NotEnoughPetrol (petrol, _) -> partial petrol origin
        | StayingAtLocation (petrol, location) -> partial petrol location
        | invalidDestiny -> invalidDestiny
    | None ->
        let result = InvalidDestiny input
        logEvent result
        result

[<EntryPoint>]
let main argv =

    let inputReader = fun () ->
        printfn "Enter you destination: home, office, stadium, gas"
        let input = Console.ReadLine()
        input.Trim()

    let logEvent (result: Result) =
        printfn "%s" result.PrettyPrint

    program inputReader logEvent 100 Home |> ignore
    //dumpHistory() |> ignore

    printfn "Program terminated."
    0
