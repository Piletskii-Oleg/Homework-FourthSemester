module Calculator

type StringCalculator() =
    member _.Bind(x: string, f) =
        let isNumber, maybeNumber = System.Int32.TryParse(x)
        match isNumber with
        | true -> f maybeNumber
        | false -> None

    member _.Return(x) = Some x