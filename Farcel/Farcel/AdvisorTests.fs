module AdvisorTests
open Advisor
open Result
open Package
open Xunit
open Swensen.Unquote
let options = [
    { Name="Large"; Width=380; Height=550; Breadth=200; Cost=8.5M }
    { Name="Small"; Width=210; Height=280; Breadth=130; Cost=5M }
    { Name="Medium"; Width=280; Height=390; Breadth=180; Cost=7.5M }
]

[<Theory>]
let ``will advise a small package if item is small`` =
    let fit = advise {Width=1; Height=1; Breadth=1; Weight=1M } options
    match fit with
    | Failure _ -> Assert.True(false, "No fit found")
    | Success p ->
        let cost = PackageCost.value p.Cost
        test <@ cost = 5M @>

[<Fact>]
let ``sanity`` = test <@ 1 = 2 @>