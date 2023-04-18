module Rounding

type Rounding(precision: int) =
    member _.Bind(x, f) =
        let rounded = System.Double.Round(x, precision)
        f rounded
        
    member _.Return(x) = System.Double.Round(x, precision)