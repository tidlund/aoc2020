open System.Text.RegularExpressions

let notEmpty s = Seq.isEmpty s |> not

let inputLines =
    System.IO.File.ReadAllLines("input.txt")
    |> Array.toList

let rec splitit (input: string list): (string list list) =
    match (List.takeWhile notEmpty input), (List.skipWhile notEmpty input) with
    | [], [] -> []
    | [], r -> r |> List.tail |> splitit
    | l, [] -> [ l ]
    | l, r -> l :: (r |> List.tail |> splitit)


let parseFields (passport: string list) =
    (passport |> List.fold (fun a b -> a + " " + b) "").Split(" ")
    |> Seq.filter (Seq.isEmpty >> not)
    |> Seq.map (fun s -> (s.Split(":").[0], s.Split(":").[1]))
    |> Map

let requiredFields =
    [ "ecl"
      "pid"
      "eyr"
      "hcl"
      "byr"
      "iyr"
      "hgt" ]

let checkOne (passport: Map<string, string>): bool =
    requiredFields
    |> List.filter (fun f -> Map.containsKey f passport)
    |> List.length
    |> (=) 7

let countCorrect (passports: Map<string, string> list): int =
    passports |> List.filter checkOne |> List.length

let checkYear min max (year: string) = (int year) >= min && (int year) <= max

let checkHeight (hgt: string): bool =
    match hgt.[((String.length hgt) - 2)..], (int hgt.[0..((String.length hgt) - 3)]) with
    | "cm", l -> l <= 193 && l >= 150
    | _, l -> l <= 76 && l >= 59

let checkRegex (re: string) (s: string) = Regex.IsMatch(s, re)

let checkFields pp =
    checkRegex "^[0-9]{4}$" (Map.find "byr" pp)
    && checkYear 1920 2002 (Map.find "byr" pp)
    && checkRegex "^[0-9]{4}$" (Map.find "iyr" pp)
    && checkYear 2010 2020 (Map.find "iyr" pp)
    && checkRegex "^[0-9]{4}$" (Map.find "eyr" pp)
    && checkYear 2020 2030 (Map.find "eyr" pp)
    && checkRegex "^([0-9]{3}cm|[0-9]{2}in)$" (Map.find "hgt" pp)
    && checkHeight (Map.find "hgt" pp)
    && checkRegex "^#[0-9a-f]{6}$" (Map.find "hcl" pp)
    && checkRegex "^(amb|blu|brn|gry|grn|hzl|oth)$" (Map.find "ecl" pp)
    && checkRegex "^[0-9]{9}$" (Map.find "pid" pp)

let countCorrect2 (passports: Map<string, string> list): int =
    passports
    |> List.filter checkOne
    |> List.filter checkFields
    |> List.length

printfn
    "1: %d"
    (splitit inputLines
     |> List.map parseFields
     |> countCorrect)

printfn
    "2: %d"
    (splitit inputLines
     |> List.map parseFields
     |> countCorrect2)
