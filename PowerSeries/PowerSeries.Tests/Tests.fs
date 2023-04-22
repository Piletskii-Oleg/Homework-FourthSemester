module PowerSeries.Tests

open FsUnit
open NUnit.Framework

[<Test>]
let ``List should be empty if m + n is less than n`` () =
    power_series 2 -2 |> should equal List.empty<int64>
    power_series 2 -1 |> should equal List.empty<int64>
    
[<Test>]
let ``List should be calculated correctly`` () =
    power_series 2 4 |> should equal [4L; 8L; 16L; 32L; 64L]
    power_series 3 1 |> should equal [8L; 16L]
    power_series 0 2 |> should equal [1L; 2L; 4L]
    