module Advisor

open Package
open Result
open ItemToSend
open System.Collections.Generic

type PackageRequest = { Width: int; Height: int; Breadth: int; Weight: decimal }
type PackageOption = { Width: int; Height: int; Breadth: int; Name: string; Cost: decimal }

let folder carry current =
    match current with
    | Success s -> s::carry
    | _ -> carry
 
let fits (item:ItemToSend.T) (package:Package.T) =
    let itemDimensions = [ (Size.value item.Width), (Size.value item.Height), (Size.value item.Breadth)]
                            |> List.sortBy (fun a -> a)
    let packageDimensions = [ (Size.value package.Width), (Size.value package.Height), (Size.value package.Breadth)]
                            |> List.sortBy (fun a -> a)

    let folder isFit d1 d2 =
        if d1 <= d2
            then isFit && true
            else false

    List.fold2 folder true itemDimensions packageDimensions

let findPackage (valids:Package.T list) item =
    try
        let fit =
            valids
            |> List.sortBy (fun x -> (Size.value x.Width) * (Size.value x.Height) * (Size.value x.Breadth))
            |> List.find (fun c -> fits item c)
        Success fit
    with
        | :? KeyNotFoundException -> Failure "No package available"
        
let advise (request: PackageRequest) (options: PackageOption list) (maxWeight:decimal) = 
    if request.Weight > maxWeight
        then Failure "Too heavy"
    else
        let validOptions =
            options
            |> List.map (fun o -> Package.create o.Name o.Width o.Height o.Breadth o.Cost)
            |> List.fold folder []

        let result = new Result.Builder()

        result 
            {
            let! i = ItemToSend.create request.Width request.Height request.Breadth request.Weight
            return findPackage validOptions i
            }
