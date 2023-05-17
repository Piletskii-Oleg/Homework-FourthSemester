module Fibonacci

let rec fibonacci n =
    if n < 0 then -1
    else
        let rec fibAdditional twoBack oneBack i =
            match i with
            | _ when i = n -> twoBack
            | _ -> fibAdditional oneBack (twoBack + oneBack) (i + 1)
        fibAdditional 0 1 0
printfn $"%d{fibonacci 4}"