module Rhombus

let getLineLength n = n + n - 1

let getStartPos currentCount lineLength = lineLength / 2 - currentCount

let getEndPos currentCount lineLength = lineLength / 2 + currentCount

let isInValidRange value currentCount length =
    value >= getStartPos currentCount length
    && value <= getEndPos currentCount length

let drawLine current total =
    let length = getLineLength total

    List.init length id
    |> List.map (fun x -> if isInValidRange x current length then '*' else ' ')

let charListToString = List.map string >> List.reduce (+)

let getRhombus n =
    let length = (getLineLength n)

    if length <= 0 then
        [ "" ]
    else
        List.init length (fun n -> if n < length / 2 then n else length - n - 1)
        |> List.map (fun i -> (drawLine i n) |> charListToString)

let drawRhombus n =
    getRhombus n |> List.iter (printfn "%s")
