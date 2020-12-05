let mapping =
    [ 'B', '1'
      'F', '0'
      'R', '1'
      'L', '0' ]
    |> Map.ofList

let seats =
    System.IO.File.ReadAllLines("input.txt")
    |> Seq.map
        (String.map (fun c -> Map.find c mapping)
         >> fun s -> "0b" + s
         >> int)

let maxSeat = seats |> Seq.max
let minSeat = seats |> Seq.min

let allSeats =
    seq { minSeat .. 1 .. maxSeat } |> Set.ofSeq

let presentSeats = Set.ofSeq seats
let diff = allSeats - presentSeats

printfn "1: %d" maxSeat
printfn "2: %d" (diff |> Seq.head)
