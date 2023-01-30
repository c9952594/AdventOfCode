module AdventOfCode2022_Day15

open System.Runtime.InteropServices

VerifyTests.VerifierSettings.AddExtraSettings
    (fun settings ->
        settings.NullValueHandling <- Argon.NullValueHandling.Include
        settings.DefaultValueHandling <- Argon.DefaultValueHandling.Include
    )

open NUnit.Framework

let input =
    [|
        "Sensor at x=3890859, y=2762958: closest beacon is at x=4037927, y=2985317"
        "Sensor at x=671793, y=1531646: closest beacon is at x=351996, y=1184837"
        "Sensor at x=3699203, y=3052069: closest beacon is at x=4037927, y=2985317"
        "Sensor at x=3969720, y=629205: closest beacon is at x=4285415, y=81270"
        "Sensor at x=41343, y=57178: closest beacon is at x=351996, y=1184837"
        "Sensor at x=2135702, y=1658955: closest beacon is at x=1295288, y=2000000"
        "Sensor at x=24022, y=1500343: closest beacon is at x=351996, y=1184837"
        "Sensor at x=3040604, y=3457552: closest beacon is at x=2994959, y=4070511"
        "Sensor at x=357905, y=3997215: closest beacon is at x=-101509, y=3502675"
        "Sensor at x=117943, y=3670308: closest beacon is at x=-101509, y=3502675"
        "Sensor at x=841852, y=702520: closest beacon is at x=351996, y=1184837"
        "Sensor at x=3425318, y=3984088: closest beacon is at x=2994959, y=4070511"
        "Sensor at x=3825628, y=3589947: closest beacon is at x=4299658, y=3299020"
        "Sensor at x=2745170, y=139176: closest beacon is at x=4285415, y=81270"
        "Sensor at x=878421, y=2039332: closest beacon is at x=1295288, y=2000000"
        "Sensor at x=1736736, y=811875: closest beacon is at x=1295288, y=2000000"
        "Sensor at x=180028, y=2627284: closest beacon is at x=-101509, y=3502675"
        "Sensor at x=3957016, y=2468479: closest beacon is at x=3640739, y=2511853"
        "Sensor at x=3227780, y=2760865: closest beacon is at x=3640739, y=2511853"
        "Sensor at x=1083678, y=2357766: closest beacon is at x=1295288, y=2000000"
        "Sensor at x=1336681, y=2182469: closest beacon is at x=1295288, y=2000000"
        "Sensor at x=3332913, y=1556848: closest beacon is at x=3640739, y=2511853"
        "Sensor at x=3663725, y=2525708: closest beacon is at x=3640739, y=2511853"
        "Sensor at x=2570900, y=2419316: closest beacon is at x=3640739, y=2511853"
        "Sensor at x=1879148, y=3584980: closest beacon is at x=2994959, y=4070511"
        "Sensor at x=3949871, y=2889309: closest beacon is at x=4037927, y=2985317"
    |]

let example =
    [|
        "Sensor at x=2, y=18: closest beacon is at x=-2, y=15"
        "Sensor at x=9, y=16: closest beacon is at x=10, y=16"
        "Sensor at x=13, y=2: closest beacon is at x=15, y=3"
        "Sensor at x=12, y=14: closest beacon is at x=10, y=16"
        "Sensor at x=10, y=20: closest beacon is at x=10, y=16"
        "Sensor at x=14, y=17: closest beacon is at x=10, y=16"
        "Sensor at x=8, y=7: closest beacon is at x=2, y=10"
        "Sensor at x=2, y=0: closest beacon is at x=2, y=10"
        "Sensor at x=0, y=11: closest beacon is at x=2, y=10"
        "Sensor at x=20, y=14: closest beacon is at x=25, y=17"
        "Sensor at x=17, y=20: closest beacon is at x=21, y=22"
        "Sensor at x=16, y=7: closest beacon is at x=15, y=3"
        "Sensor at x=14, y=3: closest beacon is at x=15, y=3"
        "Sensor at x=20, y=1: closest beacon is at x=15, y=3"
    |]

type Beacon = { x : int; y : int }
type Sensor = { x : int; y : int; beacon : Beacon ; distanceToBeacon : int }

let manhattenDistance (x1 : int) (y1 : int) (x2 : int) (y2 : int) : int =
    abs (x1 - x2) + abs (y1 - y2)

let parseInput (input : string array) : Sensor array =
    input
    |> Array.map
        (fun line ->
            let parts = line.Split(' ')
            let x = int (parts.[2].Substring(2).TrimEnd(','))
            let y = int (parts.[3].Substring(2).TrimEnd(':'))
            let bx = int (parts.[8].Substring(2).TrimEnd(','))
            let by = int (parts.[9].Substring(2))
            let distance = manhattenDistance x y bx  by
            { x = x; y = y; beacon = { x = bx; y = by } ; distanceToBeacon = distance }
        )

[<Test>]
let Day15_Part1 () =
    let row = 2000000
    
    let sensors =
        input
        |> parseInput
    
    let numberOfSensorsOrBeaconsOnRow =
        sensors
        |> Array.choose
            (fun sensor ->
                if (sensor.beacon.y = row) then
                    Some sensor.beacon.x
                else
                    None
            )
        |> Array.distinct
        |> Array.length
    
    let visibleOnRow =
        sensors
        |> Array.collect
            (fun sensor ->
                let distanceToRow =
                    manhattenDistance sensor.x sensor.y sensor.x row
                    
                let numberOfSpacesBeyondRow =
                    sensor.distanceToBeacon - distanceToRow
                    
                if (numberOfSpacesBeyondRow >= 0) then
                    [| sensor.x - numberOfSpacesBeyondRow .. sensor.x + numberOfSpacesBeyondRow |]
                else
                    [|  |]
            )
        |> Array.distinct
        |> Array.length
    
    Assert.AreEqual(4502208, visibleOnRow - numberOfSensorsOrBeaconsOnRow)
        
[<Test>]
let Day15_Part2 () =
    let sensors =
        input
        |> parseInput

    let minX = sensors |> Array.minBy (fun s -> s.x) |> (fun s -> s.x)
    let maxX = sensors |> Array.maxBy (fun s -> s.x) |> (fun s -> s.x)
    let minY = sensors |> Array.minBy (fun s -> s.y) |> (fun s -> s.y)
    let maxY = sensors |> Array.maxBy (fun s -> s.y) |> (fun s -> s.y)
    
    let (ascendingGradients, descendingGradients) =
        sensors
        |> Array.fold
            (fun (ascending, descending) sensor ->
                (*
                               top
                                |
                    left ---- sensor ---- right
                                |     
                              bottom
                *)
                
                let left = sensor.x - sensor.distanceToBeacon - 1
                let right = sensor.x + sensor.distanceToBeacon + 1
                
                (*
                    y = mx + c
                    c = y - mx
                *)  
                
                let ascendingFromLeftToTop     = sensor.y - (-1 * left)
                let ascendingFromBottomToRight = sensor.y - (-1 * right)
                
                let descendingFromLeftToBottom = sensor.y - (     left)
                let descendingFromTopToRight   = sensor.y - (     right)
                
                ascendingFromLeftToTop :: ascendingFromBottomToRight :: ascending
                ,
                descendingFromLeftToBottom:: descendingFromTopToRight :: descending
            )
            ([], [])
        
    let intersections =
        ascendingGradients
        |> List.collect
            (fun ascGrad ->
                descendingGradients
                |> List.choose
                    (fun descGrad ->
                        (*
                            y =  1x + descGrad
                            y = -1x + ascGrad
                            
                            1x + descGrad = -1x + ascGrad
                            1x = -1x + ascGrad - descGrad 
                            2x = ascGrad - descGrad
                            x = ascGrad - descGrad / 2 
                        *)
                        let x = (ascGrad - descGrad) / 2
                        
                        (*
                            y =  1x + descGrad
                            y =  x + descGrad
                        *)
                        let y = x + descGrad
                        
                        let xIsInRange =
                            (x >= minX && x <= maxX && y >= minY && y <= maxY)
                        
                        if xIsInRange then
                            Some (x,y)
                        else
                            None
                    )
            )
        
    let (x, y)  =
        intersections
        |> List.find
            (fun (x,y) ->
                sensors
                |> Array.forall
                    (fun sensor ->
                        manhattenDistance sensor.x sensor.y x y > sensor.distanceToBeacon
                    )
            )
            
    let answer = ((bigint x) * (bigint 4000000) + (bigint y)) 
        
    Assert.AreEqual(13784551204480I, answer)
        
    // VerifyNUnit.Verifier.Verify(visibleOnRows).ToTask() 
    // |> Async.AwaitTask  
    // |> Async.RunSynchronously
    // |> ignore
      

