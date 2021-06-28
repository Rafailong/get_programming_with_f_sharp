module Formaters

open Models
open Fleece.Newtonsoft

type Formater = Result<Success, Error> -> string

let defaultFormater : Formater =
    fun result ->
        match result with
        | Error err -> string <| toJson err
        | Ok success -> string <| toJson success
