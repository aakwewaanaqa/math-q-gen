using Gsat.Core;
using Gsat.Core.Interfaces;
using Gsat.Core.Maths;
using Gsat.Core.Structs;

namespace Gsat.Units;

[QuestionMaker("math", "三位以下小數的除法", 6)]
public class DivideFloating : IUnit
{
    [Question(1)]
    public Func<Question> _1 => () =>
    {
        var f = new Seq<int>([2, 3, 4]) >> new C(3, canRepeat: true);
        var p = new Seq<int>([3, 5, 7, 11]) >> new C(1);

        var bigInt = f.Product() * p.Product();
        var big    = new Floats(bigInt, bigInt.Digits());

        var smallInt = p.Product();
        var small    = new Floats(smallInt, smallInt.Digits());

        var r = big / small;
        return new QuestionBuilder(
            [
                $@"\({big} \div {small} = ?\)",
            ],
            $@"\({r}\)",
            [
                $@"\({r * 10}\)",
                $@"\({r * 100}\)",
                $@"\({r / 10}\)",
                $@"\({r / 100}\)",
            ]
        ).ToQuestion();
    };

    [Question(1)]
    public Func<Question> _2 => () =>
    {
        var o = (new Seq<int>([2, 3, 4]) >> new C(3, canRepeat: true)).Product();
        var a = new RFloat(1, 5, 1).ToFloat();
        var b = new RFloat(1, 5, 1).ToFloat();
        var x = a * b * o;

        return new QuestionBuilder(
            [
                $@"\({x} \div {a} \div {b} = ?\)",
            ],
            $@"\({o}\)",
            [
                $@"\({o * 2}\)",
                $@"\({o * 3}\)",
                $@"\({o * 4}\)",
                $@"\({o / 2}\)",
                $@"\({o / 3}\)",
                $@"\({o / 4}\)",
            ]
        ).ToQuestion();
    };
}