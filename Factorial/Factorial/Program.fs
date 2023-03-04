let factorial x =
    let rec loop i acc =
        match i with
        | 0 | 1 -> acc
        | _ -> loop (i - 1) (acc * i)
    loop x 1
printfn $"%d{match_factorial 5}"