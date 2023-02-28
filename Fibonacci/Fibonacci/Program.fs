let rec fibonacci n =
    let rec fibAdditional twoBack oneBack i =
        if i = n then twoBack
        else fibAdditional oneBack (twoBack + oneBack) (i + 1)
    fibAdditional 0 1 0