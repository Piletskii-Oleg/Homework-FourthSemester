module PowerSeries

/// Creates a list of consecutive powers of 2,
/// starting from 2^n and ending with 2^(n + m)
let power_series n m =
    let rec power_two power acc =
        match power with
        | _ when power = n -> acc
        | _ when power > n -> power_two (power - 1) (acc / 2L)
        | _ -> power_two (power + power) (acc * acc)
    let rec create_list element i acc =
        match i with
        | _ when i >= m + 1 -> acc
        | _ -> create_list (element * 2L) (i + 1) (element :: acc)
    List.rev (create_list (power_two 1 2L) 0 [])
printfn $"%A{power_series 2 4}"