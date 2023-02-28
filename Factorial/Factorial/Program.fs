let factorial x =
    let rec innerFactorial acc i =
        if i = x + 1 then acc
        else innerFactorial (acc * i) (i + 1)
    innerFactorial 1 1