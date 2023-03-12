module ParseTree

type Node<'a> =
    | Element of 'a
    | Multiply of Node<'a> * Node<'a>
    | Sum of Node<'a> * Node<'a>
    | Divide of Node<'a> * Node<'a>
    | Subtract of Node<'a> * Node<'a>

let rec calculate tree =
    match tree with
    | Element element -> element
    | Multiply (left, right) -> calculate left * calculate right
    | Sum (left, right) -> calculate left + calculate right
    | Divide (left, right) -> calculate left / calculate right
    | Subtract (left, right) -> calculate left - calculate right