let find_element ls element =
    let rec check_element ls_copy i = 
       if i = List.length ls then -1
       else if (List.head ls_copy = element) then i
       else check_element (List.tail ls_copy) (i + 1)
    check_element ls 0
printfn $"%A{find_element [1; 3; 5] 4}, %A{find_element [1; 3; 5] 5}"