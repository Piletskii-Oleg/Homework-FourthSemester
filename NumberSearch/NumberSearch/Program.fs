let find_element element ls =
    let rec check_element i ls_copy =
       match i with
       | _ when i = List.length ls -> -1
       | _ when List.head ls_copy = element -> i
       | _ -> check_element (i + 1) (List.tail ls_copy)
    check_element 0 ls
printfn $"%A{find_element 4 [1; 3; 5]}, %A{find_element 5 [1; 3; 5]}"
