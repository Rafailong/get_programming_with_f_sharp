type Address =
    { Street: string
      City: string
      State: string }

type Customer =
    { Forename: string
      Surename: string
      Age: int
      Address: Address
      EmailAddress: string }

// being explicit with the type of the record
let customer : Customer =
    { Forename = "March"
      Surename = "Evans"
      Age = 35
      Address =
          { Street = "Main Ave"
            City = "Austin"
            State = "TX" }
      EmailAddress = "marhc.evans@mail.com" }

type Car =
    { Manufacturer: string
      EngineSize: int
      NumberOfDoors: int
      Model: string
      Year: int }

let car =
    { Car.Manufacturer = "Mazda" // we can prefix the 1st property to be explicit ;)
      EngineSize = 1048
      NumberOfDoors = 5
      Model = "3 Sedan iTouring"
      Year = 2010 }
printfn "Car: %A" car

// copy-and-update syntax
// simliar to Scala' case class copy
let car' =
    { car with
          NumberOfDoors = 3
          Year = 2021 }
printfn "Car (shadowing) %A" car'
