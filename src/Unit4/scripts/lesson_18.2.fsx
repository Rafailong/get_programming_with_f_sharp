open System
open System.IO

let overSize size (fileInfo: FileInfo) = fileInfo.Length > size

let withExtension extension (fileInfo: FileInfo) = extension = fileInfo.Extension

let createdBefore dateTime (fileInfo: FileInfo) =
    let result =
        DateTime.Compare(dateTime, fileInfo.CreationTimeUtc)

    result < 0

let rules =
    [ overSize (int64 1)
      withExtension ".fsx" ]

let filterRule =
    List.reduce
        (fun r1 r2 ->
            fun (fileInfo: FileInfo) ->
                // printfn "Processing file %s (size %d)" fileInfo.Name fileInfo.Length
                r1 fileInfo && r2 fileInfo) // && lazy evaluation of right side
        rules

let filterOutFiles rule path =
    Directory.EnumerateFiles path
    |> Seq.map (fun fileName -> FileInfo(fileName))
    |> Seq.filter rule

let path = fsi.CommandLineArgs |> Array.item 1

filterOutFiles filterRule path
// filterOutFiles (withExtension ".fsx") path
// filterOutFiles (overSize (int64 1)) path
|> Seq.iter (fun fileInfo -> printfn "Keeping file %s" fileInfo.Name)

// To run this script do something like
// $ fsharpi lesson_18.2.fsx "GetProgrammingWithFSharp/src/"
