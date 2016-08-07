// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open Advisor
open Result
open Package

let readInt prompt = 
    printfn "%s" prompt
    let x = System.Console.ReadLine()
    int32 x

let readDecimal prompt = 
    printfn "%s" prompt
    let x = System.Console.ReadLine()
    decimal x
        
let options = [
    { Name="Large"; Width=380; Height=550; Breadth=200; Cost=8.5M }
    { Name="Small"; Width=210; Height=280; Breadth=130; Cost=5M }
    { Name="Medium"; Width=280; Height=390; Breadth=180; Cost=7.5M }
]

let maxWeight = 25M

[<EntryPoint>]
let main argv = 
    let request = 
        {
        Width = readInt("Width");
        Height = readInt("Height");
        Breadth = readInt("Breadth");
        Weight = readDecimal("Weight")
        }

    let result = advise request options maxWeight
    match result with
    | Success p -> 
        let (PackageName name) = p.Name
        let cost = PackageCost.value p.Cost
        printfn "%s parckage advised, cost %A" name cost
    | Failure m -> printfn "Cannot advise. %s" m

    System.Console.ReadKey() |> ignore
    
    0