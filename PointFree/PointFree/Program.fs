module PointFree

let func1 x l = List.map (fun y -> y * x) l

let func2 x = List.map (fun y -> y * x)

let func3 x = List.map (fun y -> ((*) x y))

let func4 x = List.map ((*) x)

let func5 x = (*) x |> List.map

let func6 x = x |> (*) |> List.map

let func7 x = x |> ((*) >> List.map)

let func8 = ((*) >> List.map)