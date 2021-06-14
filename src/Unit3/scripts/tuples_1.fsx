let parse (str: string) =
  let array = str.Split(' ')
  if (array.Length >= 3) then Some (array.[0], array.[1], int <| array.[2])
  else None

match parse("Mary Asteroids 2500") with
| Some(player, game, score) -> // deconstructing tuples
  printfn "Player: %s - Game: %s - Score: %d" player game score
| None -> printfn "Invalid string to parse"

// f# genericize tuples too!
// addNumbers: int * int * int * 'a -> int
let addNumber args =
  let a,b,c,_ = args
  a+b+c

open System.IO

let listFilesAsNameAndLastModifiedDate path =
  let here = new DirectoryInfo(path)
  let extractNameAndLastModificationDate (fi:FileInfo) =
    fi.Name, fi.LastWriteTimeUtc.ToShortDateString()
  Array.map extractNameAndLastModificationDate (here.GetFiles())

listFilesAsNameAndLastModifiedDate "."