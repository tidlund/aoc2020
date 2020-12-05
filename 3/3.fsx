let input = System.IO.File.ReadAllLines("input.txt")

let check (right: int) (down: int) (row: int) (str: string) =
    if row % down = 0 then str.[row * right % (Seq.length str)] else '.'

let count (right: int) (down: int) =
    input
    |> Seq.mapi (check right down)
    |> Seq.filter (fun c -> c = '#')
    |> Seq.length
    |> uint64

printfn "%d" (count 3 1)

let res2 =
    (count 1 1)
    * (count 3 1)
    * (count 5 1)
    * (count 7 1)
    * (count 1 2)

printfn "%d" res2
