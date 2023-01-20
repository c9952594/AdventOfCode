module AdventOfCode2022_Day12

VerifyTests.VerifierSettings.AddExtraSettings
    (fun settings -> 
        settings.NullValueHandling <- Argon.NullValueHandling.Include
        settings.DefaultValueHandling <- Argon.DefaultValueHandling.Include
    )

open NUnit.Framework

let input =
    [|
        "abcccaaaaacccacccccccccccccccccccccccccccccccccccccccccccccccccccccccaaaaaacaccccccaaacccccccccccccccccccccccccccccccccccaaaaaaaaccccccccccccccccccccccccccccccccaaaaaa"
        "abcccaaaaacccaaacaaacccccccccccccccccaaccccccacccaacccccccccccaacccccaaaaaaaaaaaccaaaaaaccccccccccccccccccccccccccccccccccaaaaaccccccccccccccccccccccccccccccccccaaaaaa"
        "abccccaaaaaccaaaaaaaccccccccccccccaaaacccccccaacaaacccccccccaaaaaacccaaaaaaaaaaaccaaaaaacccccccccccccccccccccccccccccccccccaaaaaccccccccccccccaaacccccccccccccccccaaaaa"
        "abccccaacccccaaaaaacccccccccccccccaaaaaacccccaaaaaccccccccccaaaaaacaaaaaaaaaaaaaccaaaaaacccccccccccccccccccccccccccccccccccaacaaccccccccccccccaaaaccccccccccccccccccaaa"
        "abccccccccccaaaaaaaacccccccccccaaccaaaaaccccccaaaaaacccccccccaaaaacaaaaaaaaccccccccaaaaacccccccccccccccccccccccccccccccccccaacccccccccccccccccaaaaccaaacccccccccccccaac"
        "abaaaaaccccaaaaaaaaaaccccccaaccaacaaaaacccccaaaaaaaaaaacccccaaaaacaaaacaaaaacccccccaacaacccccccccccccccccccccccccccccccccccccccccccccccccccccccaaaaaaaaacccccccccccaaac"
        "abaaaaaccccaaaaaaaaaacaacccaaaaaacaccaacccccaaaaaaaaaaacccccaaaaaccccccaaaaaccccccccccaacccccccccccccccccccccccccccccccccccccccccccccccccccccccaaaakkkllccccccccccccccc"
        "abaaaaacccccccaaacaaaaaaccccaaaaaaaccccccaaacccaacccaaaaaaacccccccccccccaaaaacccccccccaaaaaaccccccccccccccccccccccccccaaccccccccccccccccccccccackkkkkklllccccaaaccccccc"
        "abaaaaacccccccaaacaaaaaaacccaaaaaaaccccccaaaacaaacaaaaaaaacccccccccccccaaaaaacccccccccaaaaaaccaacaacccccccccccccccaaaaaacccccccccccccccccccaaakkkkkkkkllllcccaaacaccccc"
        "abaaaaaccccccccaacaaaaaaaacaaaaaaccccccccaaaaaaacaaaaaaaaacccccccccccccaaaacccccccccaaaaaaacccaaaaacccccccccccccccaaaaaaccccccccccccccccjjjjjkkkkkkpppplllcccaaaaaacccc"
        "abaaaccccccccccccccaaaaaaacaaaaaacccccccccaaaaaaccaaaaaaaccccccccccccccccaaaccccccccaaaaaaaccccaaaaacccccccccccccccaaaaaaaccccaaccccccjjjjjjjkkkkppppppplllcccaaaaacccc"
        "abccccccccccccccccaaaaaacccccccaaccccccaaaaaaaacccccaaaaaaccccccccccccccccccccccccccccaaaaaaccaaaaaacccccccccccccccaaaaaaaaaacaacccccjjjjjjjjjkooppppppplllcccaaacccccc"
        "abccccccccccccccccaaaaaacccccccccccccccaaaaaaaaacccaaacaaacccccccccccccccccccccccaaaccaaccaaccaaaaccccccccccccccccaaaaaaaccaaaaaccccjjjjooooooooopuuuupppllccccaaaccccc"
        "abccccccccccccccccccccaaccccccccccccccccaaaaaaaacccaaaccaacccccccccccccccccccccccaaaaaaacccccccaaaccccccccccccccccaaaaaaccccaaaaaaccjjjoooooooooouuuuuupplllccccaaccccc"
        "abccaaaaccccaaacccccccccccccccccccccccccccaaaaaaaccaaccccccccccccaacccccccccccccccaaaaacccaaccaaaccccccccccccccccccccaaacccaaaaaaaccjjjoootuuuuuuuuuuuuppllllccccaccccc"
        "abccaaaaaccaaaacccccccccccccccccccccccccccaacccacccccccccccccccacaaaacccccccccccaaaaaaacccaaaaaaacccccccccccccccccccccccccaaaaaacccciijnoottuuuuuuxxyuvpqqlmmcccccccccc"
        "abcaaaaaaccaaaacccccccaaaaccccccccccacccccaaccccaaaccccccccccccaaaaaacccccccccccaaaaaaaaccaaaaaacccccccccccccccccaacccccccaacaaacccciiinntttxxxxuxxyyvvqqqqmmmmddddcccc"
        "abcaaaaaacccaaacccccccaaaaccccaaaaaaaaccaaaaccccaacaacccccccccccaaaaccccccccccccaaaaaaaacccaaaaaaaacccccccccccccaaaaccccccccccaacccciiinntttxxxxxxxyyvvqqqqqmmmmdddcccc"
        "abcaaaaaacccccccccccccaaaacccccaaaaaacccaaaaaaaaaaaaacccccccccccaaaaccccccccccccccaaacacccaaaaaaaaacccccccccccccaaaacccccccccccccccciiinnnttxxxxxxxyyvvvvqqqqmmmdddcccc"
        "abcccaaccccccccccccccccaaccccccaaaaaacccaaaaaaaaaaaaaaccccccccccaacaccccccccccccccaaaccccaaaaaaaaaacccccccccccccaaaacccccccccccccccciiinnntttxxxxxyyyyyvvvqqqqmmmdddccc"
        "SbccccccccccccccccccccccccccccaaaaaaaaccaaaccaaaaaaaaacccccccccccccccccccccccccccccccccccaaaaaaacccccccccaacccccccccccccccccccccccccciiinntttxxxxEzyyyyyvvvqqqmmmdddccc"
        "abcccccccccccccccccccccccccccaaaaaaaaaacccccccaaaaaacccccccccccccaaacccccaacaacccccccccccccccaaaaaaccccccaacaaacccccccccccccccccccccciiinntttxxxyyyyyyyvvvvqqqmmmdddccc"
        "abcccccccccccccccccccccccccccaaaaaaaaaaccccccaaaaaaaaccccccccccccaaaccccccaaaacccccccccccccccaaaaaaccccccaaaaacccccccccccccccccccccciiinnnttxxyyyyyyyvvvvvqqqqmmmdddccc"
        "abcccccccccccccccccccccccccccacacaaacccccccccaaaaaaacccccccccccaaaaaaaacccaaaaacccccccccccccccaaaaaaaacaaaaaaccccccccccccccccccccccciiinntttxxwyyyyywwvvrrrqqmmmdddcccc"
        "abaccccccccccccccccccccccccccccccaaacccccccccaaacaaaaacccccccccaaaaaaaaccaaaaaacccccccccccccccaaaaaaaacaaaaaaacccccccccccccccccccccchhnnnttwwwwwwwyyywvrrrrnnnnmdddcccc"
        "abaccccccccccccccccccccccccccccccaaccccccccccccccaaaaaacccccccccaaaaaccccaaaacaccccccccccccccccaaaaacccccaaaaaaccccccaaaccccccaaaccchhnmmttswwwwwwywwwrrrrnnnnneeeccccc"
        "abaccccccccccccccccccccccccccccccccccccccccccccccaaaaaacccccaaccaaaaaacccccaaccccccccccccccccccaaaaaaccccaaccaaccccaaaaaacccccaaacahhhmmmsssssssswwwwwrrrnnnneeeecccccc"
        "abaaaccccccccccccccccccccccccccccaaaccccccccccccccaaaaaccccaaaccaaaaaacccccccccccccccccccccccccaaaaaaccccaaccccccccaaaaaacccaaaaaaahhhmmmmsssssssswwwwrrnnnneeeeacccccc"
        "abaaaccccccccccccccccccccccccccccaaaaaaccccccccccaaaaacaaaaaaaccaaaccaccccccccaaaaaccccccccccccaaaacacccccccccccccccaaaaacccaaaaaaahhhhmmmmssssssswwwwrrnnneeeeaacccccc"
        "abaaacccccccccccccccccccccccccccaaaaaaaccccccccccaaaaacaaaaaaaaaacccaaaaacccccaaaaacccccccccacaaaaaccacccccccccccccaaaaacccccaaaaaachhhmmmmmmmmmsssrrrrrnnneeeaaaaacccc"
        "abaccccccccccccccaaaaccccccccccaaaaaaaacccccccccccccccccaaaaaaaaacccaaaaaccccaaaaaacccccccccaaaaaaaaaacccccccccccccaaaaaccccccaaaaachhhhmmmmmmmooossrrronneeeaaaaaacccc"
        "abaccccccccccccccaaaaccccccccccaaaaaaacccccccccccccccccccaaaaaaaccccaaaaaacccaaaaaaccccaaaccaaaaaaaaaacccccccccccaaccccccccccaaaaaacchhhhhggggooooorrroonnfeeaaaaaccccc"
        "abcccccccccccccccaaaaccccccccccccaaaaaacccccccccccccccccaaaaaaccccccaaaaaacccaaaaaaccccaaaaaacaaaaaacccccccccaaccaacccccccccccaacccccchhhhggggggoooooooooffeaaaaacccccc"
        "abccccccccccccccccaacccccccccccccaaaaaacccccccaaccacccccaaaaaaacccccaaaaaaccccaaaccccccaaaaaacaaaaaacccccccccaaaaacccccccccccccccccccccccgggggggggooooooffffaaaaaaccccc"
        "abccccccccccccccccccccccccccaaaccaacccccccccccaaaaacccccaaccaaacccccccaaacccccccccccccaaaaaaacaaaaaacccccccccaaaaaaaaccccccccccccccccccccccaaaggggfooooffffccccaacccccc"
        "abaaccccccccccccccccccccccccaaacaccccccccccccaaaaacccccccccccaacccccccaaaacccaacccccccaaaaaaaaaaaaaaaccccccccccaaaaacccccccccccccaaaccccccccccccggfffffffffcccccccccccc"
        "abaaccccccccccccccccccccccaacaaaaacccccccccccaaaaaacccccccccccccccccccaaaacaaaacccccccaaaaaaaaaccccaccccccccccaaaaaccccccccccccccaaaccccccccccccagfffffffccccccccccccca"
        "abaacccccccaacccccccccccccaaaaaaaaccccccaacccccaaaacccccccccccccccccccaaaaaaaaacccccccccaaacaacaaacccccccccccaaacaaccccaaccaaccaaaaaaaaccccccccaaaccffffcccccccccccccaa"
        "abaaaaaaaccaaccccccccccccccaaaaacccccccaaaacccaaccccccccccccccccccacaaaaaaaaaaccccccccccaaacaaaaaacccccccccccccccaaccccaaaaaaccaaaaaaaacccccccccaacccccccccccccccaaacaa"
        "abaaaaaaaaaaccccccccccccccccaaaaaccccccaaaacccccccccccccccccccccccaaaaaaaaaaaaccccccccccccccaaaaaacccccccccccccccccccccaaaaaacccaaaaaacccccccccaaacccccccccccccccaaaaaa"
        "abaaaacaaaaaaaacccccccccccccaacaaccccccaaaaccccccccccccccccccccccccaaaaaaaaaaacccccccccccccaaaaaaaacccccccccccccccccccaaaaaaaaccaaaaaaccccccccccccccccccccccccccccaaaaa"
    |]

let example =
    [|
        "Sabqponm"
        "abcryxxl"
        "accszExk"
        "acctuvwj"
        "abdefghi"
    |]

type Distance = int
type Elevation = char
type FromElevation = int
type ToElevation = int

type Point =
    {
        X: int
        Y: int
    }

let mapAsDijkstra 
    (canStep : FromElevation -> ToElevation -> bool)
    (startPoint : Point)
    (elevations : Map<Point, (Elevation * Distance)>)
    : Map<Point, (Elevation * Distance)>
    = 
    Seq.last
    <| Seq.unfold 
        (fun ((searching : Point seq), (elevationMap : Map<Point, (Elevation * Distance)>)) -> 
            if (searching |> Seq.isEmpty) then
                None
            else
                let neighboursToUpdate = 
                    searching
                    |> Seq.collect
                        (fun currentPoint ->
                            let (currentElevation, _) = 
                                elevationMap
                                |> Map.find currentPoint

                            let neighbours = 
                                [ 
                                    { X = currentPoint.X - 1; Y = currentPoint.Y } 
                                    { X = currentPoint.X + 1; Y = currentPoint.Y }
                                    { X = currentPoint.X; Y = currentPoint.Y - 1 }
                                    { X = currentPoint.X; Y = currentPoint.Y + 1 } 
                                ]

                            let neighboursOnTheMap =   
                                neighbours
                                |> Seq.filter 
                                    (fun neighbourPoint -> 
                                        elevationMap
                                        |> Map.containsKey neighbourPoint
                                    )

                            let traversableNeighbours =     
                                neighboursOnTheMap
                                |> Seq.filter 
                                    (fun neighbourPoint -> 
                                        let (neighbourElevation, _) = 
                                            elevationMap 
                                            |> Map.find neighbourPoint

                                        canStep 
                                            (int currentElevation) 
                                            (int neighbourElevation)
                                    )

                            traversableNeighbours
                            |> Seq.map
                                (fun neighbourPoint ->
                                    (currentPoint, neighbourPoint)
                                )
                        )

                let (newSearching, newElevationMap) =
                    neighboursToUpdate
                    |> Seq.fold 
                        (fun 
                            (newSearching', newElevationMap') 
                            (currentPoint, neighbourPoint) 
                            -> 
                            let (_, distance) = newElevationMap' |> Map.find currentPoint
                            let newDistance = distance + 1

                            let (neighbourElevation, neighbourDistance) = newElevationMap' |> Map.find neighbourPoint

                            if newDistance < neighbourDistance  then
                                newSearching'
                                |> Seq.append [neighbourPoint]
                                ,
                                newElevationMap'
                                |> Map.add neighbourPoint (neighbourElevation, newDistance)
                            else
                                (newSearching', newElevationMap')
                        ) 
                        (Seq.empty, elevationMap)
                
                Some ((elevationMap) , (newSearching, newElevationMap))
        ) 
        ( [startPoint] , elevations )

let maxDistance = 999

let elevations = 
    input
    |> Seq.mapi (fun y row ->
        row
        |> Seq.mapi (fun x elevation ->
            { X = x; Y = y }, (elevation, maxDistance)
        )
    )
    |> Seq.concat
    |> Map.ofSeq

let sPoint = 
    elevations 
    |> Map.findKey (fun _ (elevation,_) -> elevation = 'S')

let ePoint = 
    elevations 
    |> Map.findKey (fun _ (elevation,_)  -> elevation = 'E')

[<Test>]
let Day12_Part1 () = 
    let adjustedElevations = 
        elevations
        |> Map.add sPoint ('a', 0)
        |> Map.add ePoint ('z', maxDistance)
    
    let walkForwards fromElevation toElevation = 
        (toElevation - fromElevation) < 2

    let mapAfterDijkstra =
        mapAsDijkstra
            walkForwards
            sPoint
            adjustedElevations

    let minDistance = 
        mapAfterDijkstra
        |> Map.find ePoint
        |> snd

    Assert.AreEqual( 517 , minDistance)

[<Test>]
let Day12_Part2 () = 
    let adjustedElevations = 
        elevations
        |> Map.add sPoint ('a', maxDistance)
        |> Map.add ePoint ('z', 0)
    
    let walkBackwards toElevation fromElevation = 
        (toElevation - fromElevation) < 2

    let mapAfterDijkstra =
        mapAsDijkstra 
            walkBackwards
            ePoint 
            adjustedElevations

    let minDistance = 
        mapAfterDijkstra
        |> Map.toSeq
        |> Seq.filter (fun (_, (elevation, _)) -> elevation = 'a')
        |> Seq.map snd
        |> Seq.map snd
        |> Seq.min

    Assert.AreEqual( 512 , minDistance)

    // let asPrintableMap =
    //     mapAfterDijkstra
    //     |> Map.toSeq
    //     |> Seq.sortBy (fun (point, _) -> point.Y, point.X)
    //     |> Seq.groupBy (fun (point, _) -> point.Y)
    //     |> Seq.map (fun (y, points) ->
    //         points
    //         |> Seq.sortBy (fun (point, _) -> point.X)
    //         |> Seq.map (fun (point, (elevation , distance)) ->
    //             let distanceAsStringWithAtLeastThreeDigits = 
    //                 distance
    //                 |> string
    //                 |> fun s -> 
    //                     if s.Length = 1 then
    //                         "00" + s
    //                     elif s.Length = 2 then
    //                         "0" + s
    //                     else
    //                         s

    //             $"{elevation}:{  distanceAsStringWithAtLeastThreeDigits } "
    //         )
    //         |> String.concat ""
    //     )
    //     |> String.concat "\n"
        

    // VerifyNUnit.Verifier.Verify(asPrintableMap).ToTask() 
    // |> Async.AwaitTask  
    // |> Async.RunSynchronously
    // |> ignore