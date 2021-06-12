let number = 100

let host = System.Uri "https://google.com"
let printHost() = printfn "%A" host.Host

let add a b = a + b
let add' (a, b) = a + b

add 1, 2 |> ignore
add' (1, 2) |> ignore

// does not compile
// let add'' (a: int, b: string) =
//   let s = a + b
//   s

// type inference not always works
// does not compile due to there is no snough info about name so we can't tell that name has a Length member
// let getLenght name = sprintf "Name is $%d chars long." name.Length
// we are being explicit with the name type
let getLenght (name: string) = sprintf "Name is $%d chars long." name.Length
// referencing getLenght helps here and we do not need to specify name type
let getLenght' name = getLenght name

open System.Collections.Generic

//  something cool:  automatic Generalization of a function
let createList a =
  let ls = List()
  ls.Add a
  ls


let sayHello(someValue) =

    let innerFunction(number) =
        if number > 10 then "Isaac"
        elif number > 20 then "Fred"
        else "Sara"

    let resultOfInner =
        if someValue < 10.0 then innerFunction(5)
        else innerFunction(15)

    "Hello " + resultOfInner

sayHello(10.5)  |> ignore
