module Advisor

open Package
open Result
open ItemToSend
open System.Collections.Generic

let folder carry current =
    match current with
    | Success s -> s::carry
    | _ -> carry

let availablePackages = [
    (Package.create "Small" 210 280 130 5M)
    (Package.create "Medium" 280 390 180 7.5M)
    (Package.create "Large" 380 550 200 8.5M)
] 
 
let validPackages = availablePackages |> List.fold folder []

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

let advise (valids:Package.T list) item =
    try
        let fit =
            valids
            |> List.sortBy (fun x -> (Size.value x.Width) * (Size.value x.Height) * (Size.value x.Breadth))
            |> List.find (fun c -> fits item c)
        Success fit.Cost
    with
        | :? KeyNotFoundException -> Failure "No package available"
        

let result = new Result.Builder()

let fit =
    result 
        {
        let! i = ItemToSend.create 1150 1 1 1.2M
        return advise validPackages i
        }
match fit with 
| Success s -> 
    printf "Pay %A and we have a deal" (PackageCost.value s)
| Failure m -> printf "Error %A" m