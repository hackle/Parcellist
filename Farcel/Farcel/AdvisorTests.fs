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
[<InlineData(1,1,1,50)>]
[<InlineData(210,280,130,50)>]
[<InlineData(210,130,280,50)>] //rotate
[<InlineData(130,280,210,50)>] //rotate again
[<InlineData(211,280,130,75)>] //slightly bigger
[<InlineData(279,389,179,75)>] //just below medium
[<InlineData(280,390,180,75)>] //just medium
[<InlineData(380,550,200,85)>] //just large
let ``will advise tightest fit`` (w:int) (h:int) (b:int) (expectedCost:int) =
    let fit = advise {Width=w; Height=h; Breadth=b; Weight=1M } options 2M
    match fit with
    | Failure _ -> Assert.True(false, "No fit found")
    | Success p ->
        let cost = PackageCost.value p.Cost
        test <@ cost = (decimal expectedCost)/10M @>

[<Theory>]
[<InlineData(381,551,201)>] //too large
let ``cannot advise if too large`` (w:int) (h:int) (b:int) =
    let fit = advise {Width=w; Height=h; Breadth=b; Weight=1M } options 2M
    match fit with
    | Success _ -> Assert.True(false, "Should fail")
    | Failure m -> test <@ m.Contains("No package") @>

[<Theory>]
[<InlineData(25, 25)>]
[<InlineData(24, 25)>]
let ``can take weight under or equal max weight`` (w:int) (max:int) =    
    let fit = advise {Width=1; Height=1; Breadth=1; Weight=(decimal w)/10M } options ((decimal max)/10M)
    match fit with
    | Success _ -> Assert.True(true)
    | Failure m -> Assert.True(false, "Should not fail")

[<Theory>]
[<InlineData(26, 25)>]
let ``cannot take weight over max weight`` (w:int) (max:int) =    
    let fit = advise {Width=1; Height=1; Breadth=1; Weight=(decimal w)/10M } options ((decimal max)/10M)
    match fit with
    | Success _ -> Assert.True(false, "Should fail")
    | Failure m -> test <@ m.Contains("Too heavy") @>