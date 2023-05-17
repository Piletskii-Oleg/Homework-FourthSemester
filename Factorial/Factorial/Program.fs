module Factorial

let factorial x =
    if x < 0 then -1L
    else
         let rec loop i acc =
            match i with
            | 0L | 1L -> acc
            | _ -> loop (i - 1L) (acc * i)
         loop x 1L
printfn $"%d{factorial 5}"