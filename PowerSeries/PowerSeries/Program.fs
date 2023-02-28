let power_series n m =
    let rec power_two acc power =
        if power = n then acc
        else if power > n then power_two (acc / 2L) (power - 1)
        else power_two (acc * acc) (power + power)
    let rec create_list acc element i =
        if i = m + 1 then acc
        else create_list (element :: acc) (element * 2L) (i + 1)
    List.rev (create_list [] (power_two 2L 1) 0)
printfn $"%A{power_series 2 4}"