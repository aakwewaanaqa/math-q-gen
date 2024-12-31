module unit_test

open NUnit.Framework
open Gsat.Structs


[<Test>]
let Test1 () =
    let factors = [2;3;5]
    let lb =
        ListBuilder()
        |> (_.Pick(factors, 0, 1))
    Assert.That(lb.Count, Is.EqualTo(1))
    ()
