open System

let printUtcNow =
  printfn "%A" DateTime.UtcNow

let greeting name age =
  sprintf "Hello %s, You are %i years old." name age

let countWords (str: string) =
  let count = Array.length <| str.Split ' '
  IO.File.WriteAllText ("./count.txt", (sprintf "'%s' - Has %d words" str count))

printUtcNow |> ignore
printfn "%s" (greeting "rafa" 32)
countWords "some text here 1 2 3 4 5"