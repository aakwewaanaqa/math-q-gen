module unit_test

open NUnit.Framework
open Gsat.Structs


[<Test>]
let Test1 () =
    let lb =
        ListBuilder<int>()
        |> (+) 2
    Assert.That(lb.Count, Is.EqualTo(1))
    ()
