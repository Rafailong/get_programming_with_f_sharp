type Customer = { Age: int }

let printCustomerAge writer customer =
  if customer.Age < 13 then writer "Child"
  elif customer.Age < 21 then writer "Teenager"
  else writer "Adult"

let stout str = printfn "%s" str
let printCustomerAgeToStdOut customer = printCustomerAge stout

printCustomerAgeToStdOut { Customer.Age = 12}
printCustomerAgeToStdOut { Customer.Age = 20}
printCustomerAgeToStdOut { Customer.Age = 30}

let toFile path str =
  System.IO.File.WriteAllText(path, str)
let printCustomerAgeToFile customer =
  printCustomerAge (toFile "./tmp.file.txt") customer

printCustomerAgeToFile { Customer.Age = 12}