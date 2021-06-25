let sum inputs =
    let mutable accumulator = 0

    for input in inputs do
        accumulator <- accumulator + input

    accumulator

let lenght inputs =
    let mutable accumulator = 0

    for _ in inputs do
        accumulator <- accumulator + 1

    accumulator

let max inputs =
    let mutable accumulator : Option<int> = None
    let f input = fun m -> if input > m then input else m

    for input in inputs do
        if accumulator.IsNone then
            accumulator <- Some input
        else
            accumulator <- Option.map (f input) accumulator

    accumulator


// using fold

let sum' : int seq -> int = Seq.fold (+) 0

let length' : int seq -> int = Seq.fold (fun s _ -> s + 1) 0

let max' : int seq -> int option =
    let comparison input = fun m -> if input > m then input else m

    let f (accumulator: int option) (input: int) =
        if accumulator.IsNone then
            Some input
        else
            Option.map (comparison input) accumulator

    Seq.fold f None
