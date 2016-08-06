module Result

    type Result<'TSuccess, 'TFailure> =
        | Success of 'TSuccess
        | Failure of 'TFailure

    type Builder() =
        member this.Bind(r, f) = 
                match r with
                | Success s -> f s
                | Failure m -> Failure m

        member this.Return(r) = r