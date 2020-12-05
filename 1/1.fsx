let nrs =
    System.IO.File.ReadAllLines("input.txt")
    |> Seq.map int
    |> Set

let res1 =
    Seq.allPairs nrs nrs
    |> Seq.find (fun (a, b) -> a + b = 2020)
    |> (fun (a, b) -> a * b)

let res2 =
    Seq.allPairs nrs nrs
    |> Seq.allPairs nrs
    |> Seq.find (fun (a, (b, c)) -> a + b + c = 2020)
    |> (fun (a, (b, c)) -> a * b * c)

printfn "1: %d" res1
printfn "2: %d" res2
