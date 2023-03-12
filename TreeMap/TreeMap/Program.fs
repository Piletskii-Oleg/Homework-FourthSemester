module TreeMap

type Tree<'a> =
    | Node of 'a * Tree<'a> * Tree<'a>
    | LeftBranch of 'a * Tree<'a>
    | RightBranch of 'a * Tree<'a>
    | Tip of 'a
    
let rec tree_map func tree =
    match tree with
    | Node(elem, left, right) -> Node(func elem, tree_map func left, tree_map func right)
    | LeftBranch (elem, left) -> LeftBranch(func elem, tree_map func left)
    | RightBranch(elem, right) -> RightBranch(func elem, tree_map func right)
    | Tip elem -> Tip (func elem)