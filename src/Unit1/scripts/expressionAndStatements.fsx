open System

let unit' = ()

let describeAge age =
    let ageDescription =
        if age < 18 then "Child!"
        elif age < 65 then "Adult!"
        else "OAP!"
    let greeting = "Hello"
    Console.WriteLine("{0}! You are a '{1}'.", greeting, ageDescription)

unit' = describeAge 30

// unit is no a proper .Net object; following line throws NullReferenceException
// unit'.GetHashCode()