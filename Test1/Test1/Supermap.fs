module Supermap

let supermap mapping list = list |> List.map mapping |> List.concat
