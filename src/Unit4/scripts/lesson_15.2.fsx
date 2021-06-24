type Descriptor = { Name: string; Size: int64 }

let func (path: string) fileName =
    let filePath = System.IO.Path.Combine(path, fileName)
    let fileInfo = System.IO.FileInfo(filePath)

    { Descriptor.Name = fileInfo.Name
      Size = fileInfo.Length }

let tree (path: string) : Descriptor list =
    System.IO.Directory.GetFileSystemEntries path
    |> Array.map (func path)
    |> Array.toList

let path = fsi.CommandLineArgs |> Array.item 1

printfn "tree of: %s" path
tree path |> List.iter (fun d -> printfn "%A" d)

// To run this script do something like
// > fsharpi lesson_15.2.fsx "GetProgrammingWithFSharp/src/Unit4/scripts/"
