module MiniCrawler.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let ``Getting page sizes should work correctly`` () =
    "https://learn.microsoft.com/ru-ru/dotnet/standard/base-types/regular-expression-language-quick-reference"
    |> sizes
    |> should
        equal
        [ ("https://learn.microsoft.com/en-us/lifecycle/faq/internet-explorer-microsoft-edge", 63013)
          ("https://download.microsoft.com/download/D/2/4/D240EBF6-A9BA-4E4F-A63F-AEB6DA0B921C/Regular%20expressions%20quick%20reference.docx",
           50869)
          ("https://download.microsoft.com/download/D/2/4/D240EBF6-A9BA-4E4F-A63F-AEB6DA0B921C/Regular%20expressions%20quick%20reference.pdf",
           643441)
          ("https://download.microsoft.com/download/D/2/4/D240EBF6-A9BA-4E4F-A63F-AEB6DA0B921C/Regular%20expressions%20quick%20reference.docx",
           50869)
          ("https://download.microsoft.com/download/D/2/4/D240EBF6-A9BA-4E4F-A63F-AEB6DA0B921C/Regular%20expressions%20quick%20reference.pdf",
           643441) ]
