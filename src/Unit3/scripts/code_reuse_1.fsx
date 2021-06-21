type Customer = { Age: int }

let where filter elems =
    seq {
        for e in elems do
            if filter e then yield e
    }

let customers =
    [ { Age = 21 }
      { Age = 35 }
      { Age = 36 } ]

let isOver35 customer = customer.Age > 35
customers |> where isOver35 |> ignore // same as: where isOver35 customers |> ignore

where (fun c -> c.Age > 35) customers |> ignore // using lambdas
