module Program

open System.Net.Http
open System.Text.RegularExpressions
open System.Threading

let nameRegex = Regex(@"<title>[\w|\s|\d|\P{Lu}|\P{IsCyrillic}]*</title>")
let onPageLinksRegex = Regex(@"<a href=""https://(\S*)""", RegexOptions.Compiled)
let linksRegex = Regex(@"https://(\S*[^""])", RegexOptions.Compiled)

let getPageContentsAsync (url: string) =
    async {
        use client = new HttpClient()
        return! client.GetStringAsync url |> Async.AwaitTask
    }

let getSizeAsync (url: string) =
    async {
        let! html = getPageContentsAsync url
        printfn $"%d{Thread.CurrentThread.ManagedThreadId}"
        return html.Length
    }

let crawlAsync (url: string) =
    async {
        let! html = getPageContentsAsync url
        let onPageLinks = onPageLinksRegex.Matches html
        html 
        |> nameRegex.Matches
        |> Seq.map (fun x -> x.Value)
        |> Seq.iter (printfn "%s")

        let links =
            seq {
                let i = onPageLinks.GetEnumerator() in

                while i.MoveNext() do
                    yield i.Current
            }
            |> Seq.cast<Match>
            |> Seq.map (fun x -> x.Value)
            |> Seq.map linksRegex.Match

        return! links |> Seq.map (fun x -> x.Value) |> Seq.map getSizeAsync |> Async.Parallel
    }

let links =
    crawlAsync "https://learn.microsoft.com/ru-ru/dotnet/standard/base-types/regular-expression-language-quick-reference"
    |> Async.RunSynchronously

let a = "<title>e 5 q3wqq34.</title>"
printfn $"%A{nameRegex.Match a}"
printfn $"%A{links}"
