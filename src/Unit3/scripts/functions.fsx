open System
open System.IO

// curried function
let writeToFile (dt: DateTime) fileName text =
  let path = sprintf "%s-%s.txt" (dt.ToShortDateString()) fileName
  File.WriteAllText(path, text)

// partially applied functions
let writeTodayFile =
  writeToFile DateTime.UtcNow

let writeTodayXmlReport =
  writeTodayFile "report.xml"

let checkCreation (dt:DateTime): unit = raise (new NotImplementedException("stub function"))

// pipe-lining
Directory.GetCurrentDirectory()
|> Directory.GetCreationTime
|> checkCreation

// composing
let f: unit -> unit =
  Directory.GetCurrentDirectory
  >> Directory.GetCreationTime
  >> checkCreation
// f()