open System
open System.IO

let buildLookUp path =
    let now = DateTime.UtcNow
    let directoryInfo = fun d -> DirectoryInfo(d)

    let extractDetails =
        fun (di: DirectoryInfo) -> di.Name, di.CreationTimeUtc

    Directory.EnumerateDirectories path
    |> Seq.map (directoryInfo >> extractDetails)
    |> Map.ofSeq
    |> Map.map (fun _ dt -> now - dt)

let path = fsi.CommandLineArgs |> Array.item 1

printfn "tree of: %s" path

buildLookUp path
|> Map.iter (fun k v -> printfn "Directory %s is %A old" k v)

// To run this script do something like
// > fsharpi lesson_17.1.fsx "GetProgrammingWithFSharp/src/"
