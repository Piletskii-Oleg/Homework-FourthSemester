module Factorial

let factorial x =
    let rec loop i acc =
        match i with
        | 0L | 1L -> acc
        | _ -> loop (i - 1L) (acc * i)
    loop x 1
printfn $"%d{factorial 5L}"