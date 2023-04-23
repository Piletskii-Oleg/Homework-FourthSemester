module Program

let printSizes link =
    link
    |> MiniCrawler.sizes
    |> List.iter (fun (link, count) -> printfn $"%s{link} - %d{count}")
