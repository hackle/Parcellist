module Package
open Result

type PackageName = PackageName of string

module Size =
    open Result
    type T = private PackageSize of int

    let create s = 
        if s > 0 
            then Success (PackageSize s)
            else Failure "Size is not valid"

    let value s =
        let (PackageSize v) = s
        v

module SizeTests =
    open Size
    open Xunit
    open Swensen.Unquote
    open Result

    [<Theory>]
    [<InlineData(15)>]
    [<InlineData(150)>]
    [<InlineData(15000)>]
    let ``Positive numbers result in success`` (size: int) =
        let actual = Size.create size
        
        match actual with
        | Success s ->         
            let result = Size.value s
            test <@ result = size @>
        | Failure s -> Assert.True(false, "Should not fail")

    [<Theory>]
    [<InlineData(0)>]
    [<InlineData(-1)>]
    let ``Non positive numbers result in failure`` (size: int) =
        let actual = Size.create size
        
        match actual with
        | Success s -> Assert.True(false, "Should not succeed")       
        | Failure m -> test <@ m.Contains("not valid") @>

module PackageCost =
    open Result
    type T = private PackageCost of decimal

    let create c =
        if c >= 0M
            then Success (PackageCost c)
            else Failure "Cost is not valid"

    let value c =
        let (PackageCost p) = c
        p

module PackageCostTests =
    open Size
    open Xunit
    open Swensen.Unquote

    [<Theory>]
    [<InlineData(1234)>]
    [<InlineData(0)>]
    let ``Non-negative cost is allowed`` (c: decimal) =
        let cost = PackageCost.create c
        match cost with
        | Success suc ->
            let actual = PackageCost.value suc
            test <@ actual = c @>
        | Failure _ -> Assert.True(false, "Should not fail")

    [<Theory>]
    [<InlineData(-1)>]
    let ``Negative cost is not cool`` (c: decimal) =
        let cost = PackageCost.create c
        match cost with
        | Success _ -> Assert.True(false, "Should not succeed")
        | Failure s -> test <@ s.Contains("not valid") @>

module Weight =
    type T = private Weight of decimal

    let create w =
        if w >= 0M
            then Success (Weight w)
            else Failure "Weight is not valid"

module Package = 
    type T = 
            {
            Name: PackageName;
            Width: Size.T;
            Height: Size.T;
            Breadth: Size.T;
            Cost: PackageCost.T;
            }
            
    let create name width height breadth cost =        
        let result = new Result.Builder()
        let n = PackageName name
        result
            {        
            let! c = PackageCost.create cost
            let! d1 = Size.create width
            let! d2 = Size.create height
            let! d3 = Size.create breadth
            return Success { Name=n; Width=d1; Height=d2; Breadth=d3; Cost=c}
            }

module ItemToSend =
    type T = 
            {
            Width: Size.T;
            Height: Size.T;
            Breadth: Size.T;
            Weight: Weight.T;
            }

    let create width height breadth weight =
        let result = new Result.Builder()
        result
            {
            let! d1 = Size.create width
            let! d2 = Size.create height
            let! d3 = Size.create breadth
            let! w = Weight.create weight
            return Success {Width=d1;Height=d2;Breadth=d3;Weight=w}
            }