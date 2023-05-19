module Brackets

open Stack

type Symbol =
    | OpeningSquare
    | ClosingSquare
    | OpeningParentheses
    | ClosingParentheses
    | OpeningCurly
    | ClosingCurly
    | Other of char

let transformElement element =
    match element with
    | '{' -> OpeningCurly
    | '}' -> ClosingCurly
    | '(' -> OpeningParentheses
    | ')' -> ClosingParentheses
    | '[' -> OpeningSquare
    | ']' -> ClosingSquare
    | symbol -> Other symbol

let filterElement element =
    match element with
    | Other _ -> false
    | _ -> true

let parse: string -> List<Symbol> =
    Seq.toList >> List.map transformElement >> List.filter filterElement

let matchSymbol symbol option func =
    match option with
    | None -> false
    | Some value when symbol = value -> func
    | _ -> false

let rec checkElements stack seq =
    let checkNext stack seq =
        checkElements (pop stack) (List.tail seq)

    match seq with
    | [] when stack <> Stack [] -> false
    | [] -> true
    | _ ->
        match List.head seq with
        | OpeningCurly
        | OpeningParentheses
        | OpeningSquare -> checkElements (push (Seq.head seq) stack) (List.tail seq)
        | ClosingCurly -> matchSymbol OpeningCurly (peek stack) (checkNext stack seq)
        | ClosingParentheses -> matchSymbol OpeningParentheses (peek stack) (checkNext stack seq)
        | ClosingSquare -> matchSymbol OpeningSquare (peek stack) (checkNext stack seq)
        | Other _ -> false

let check sequence =
    sequence |> parse |> checkElements (Stack [])
