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

module PackageCost =
    open Result
    type T = private PackageCost of decimal

    let create c =
        if c > 0M
            then Success (PackageCost c)
            else Failure "Cost is not valid"

    let value c =
        let (PackageCost p) = c
        p

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