module MiniCrawler

open System.Net.Http
open System.Text.RegularExpressions

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
        return html.Length
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

        let! sizes = links |> Seq.map getSizeAsync |> Async.Parallel
        return (Seq.zip links sizes)
    }

let sizes link =
    link |> crawlAsync |> Async.RunSynchronously |> Seq.toList
