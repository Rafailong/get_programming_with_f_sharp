// by default f# data in immutbale
let name = "Joe"
name = "Moe"

// we need to be explicit when being mutbale
let mutable age = 30
age <- 31
age = 31 // true

// 6.3.1

type Distance =
    | Short
    | Medium
    | Far

// mutable driving
let mutable petrol = 100.0

let drive distance =
    match distance with
    | Short -> petrol <- petrol - 1.0
    | Medium -> petrol <- petrol - 10.0
    | Far -> petrol <- petrol - 20.0

drive Short
drive Far
drive Medium

petrol

// immutable driving
let petrol' = 100.0

let drive' petrol distance =
    match distance with
    | Short -> petrol - 1.0
    | Medium -> petrol - 10.0
    | Far -> petrol - 20.0

let p1 = drive' petrol' Short
let p2 = drive' p1 Far
let p3 = drive' p2 Medium

let shortDive petro = petro - 1.0
let mediumDrive petro = petro - 10.0
let farDrive petro = petro - 20.0

let p4 =
    farDrive petrol' |> mediumDrive |> shortDive

p3 = p4

// working with BCL objects (inherently mutable)
open System.Net
open System.Net.Cache

// This shortcut is somewhat similar to object initializers in C#,
// except that in F# it works by making properties appear as optional constructor arguments.
let client =
    new WebClient(CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore))
client.DownloadData (new System.Uri("https://google.com")) |> ignore
client.Dispose
