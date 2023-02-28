let reverse_list ls =
    let rec reverse acc ls_copy i =
        if i = List.length ls then acc
        else reverse (List.head ls_copy :: acc) (List.tail ls_copy) (i + 1)
    reverse [] ls 0
printfn $"%A{reverse_list [0; 1; 4; 64; 123; 3]}"