let dump a =
    printfn "%A" a
    a

let checkPass (min: int) (max: int) (char: char) (password: string) =
    let count =
        password
        |> Seq.filter (fun c -> c = char)
        |> Seq.length

    count >= min && count <= max

let checkPass2 (posA: int) (posB: int) (char: char) (password: string) =
    password.[posA - 1]
    <> password.[posB - 1]
    && (password.[posA - 1] = char
        || password.[posB - 1] = char)

let passwords =
    System.IO.File.ReadAllLines("input.txt")
    |> Array.map (fun s -> s.Replace("-", " "))
    |> Array.map (fun s -> s.Replace(":", ""))
    |> Array.map (fun s -> s.Split(" "))

let res1 =
    passwords
    |> Array.filter (fun r -> (checkPass (int r.[0]) (int r.[1]) r.[2].[0] r.[3]))
    |> Array.length

let res2 =
    passwords
    |> Array.filter (fun r -> (checkPass2 (int r.[0]) (int r.[1]) r.[2].[0] r.[3]))
    |> Array.length

printfn "%d" res1
printfn "%d" res2
