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

let getTitleAsync (url: string) =
    async {
        let! html = getPageContentsAsync url
        return nameRegex.Match html
    }

let matchCollectionToSeq (matches: MatchCollection) =
    seq {
        let i = matches.GetEnumerator() in

        while i.MoveNext() do
            yield i.Current
    }

let crawlAsync (url: string) =
    async {
        let! html = getPageContentsAsync url
        let onPageLinks = onPageLinksRegex.Matches html

        let links =
            onPageLinks
            |> matchCollectionToSeq
            |> Seq.cast<Match>
            |> Seq.map (fun x -> x.Value)
            |> Seq.map linksRegex.Match
            |> Seq.map (fun x -> x.Value)

        let! titles = links |> Seq.map getTitleAsync |> Async.Parallel

        titles |> Array.map string |> Array.filter (fun s -> s <> "") |> Array.iter (printfn "%s")

        let! sizes = links |> Seq.map getSizeAsync |> Async.Parallel
        return (Seq.zip links sizes)
    }

let sizes =
    crawlAsync
        "https://learn.microsoft.com/ru-ru/dotnet/standard/base-types/regular-expression-language-quick-reference"
    |> Async.RunSynchronously
    |> Seq.toList

printfn $"%A{sizes}"
