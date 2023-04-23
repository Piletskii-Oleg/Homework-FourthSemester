module Lazy

open System.Threading
open ILazy

type UnsafeLazy<'a>(supplier: unit -> 'a) =
    let mutable result: 'a Option = None

    let get () =
        match result with
        | None ->
            result <- Some(supplier ())
            result.Value
        | Some value -> value

    interface ILazy<'a> with
        member _.Get() = get ()

type SafeLazy<'a>(supplier: unit -> 'a) =
    let lockObj = obj ()

    let mutable result: 'a Option = None

    let get () =
        if result.IsSome then
            result.Value
        else
            lock lockObj (fun _ ->
                match result with
                | None ->
                    result <- Some(supplier ())
                    result.Value
                | Some value -> value)

    interface ILazy<'a> with
        member _.Get() = get ()

type LockFreeLazy<'a>(supplier: unit -> 'a) =
    let mutable result: 'a Option = None

    let rec get () =
        if result.IsSome then
            result.Value
        else
            let current = result

            let calculated = Some(supplier ())

            if
                not
                <| obj.ReferenceEquals(current, Interlocked.CompareExchange(&result, calculated, current))
            then
                get ()
            else
                result.Value

    interface ILazy<'a> with
        member this.Get() = get ()
