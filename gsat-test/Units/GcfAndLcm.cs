using Gsat.Structs;

namespace Gsat.Units;

/// <summary>
///     最大公因數與最小公倍數
/// </summary>
[QuestionMaker("math", "最大公因數與最小公倍數", 6)]
public class GcfAndLcm : IUnit
{
    [Question(difficulty: 1)]
    public Func<Question> _1 => () =>
    {
        var factors = new int[4];
        MathG.GetRandom(2, 10, factors);
        var a      = factors[0] * factors[1] * factors[2];
        var b      = factors[0] * factors[1] * factors[3];
        var answer = factors[0] * factors[1];
        var selections = new List<string>()
        {
            $@"\({factors[0] * factors[1]}\)",
            $@"\({factors[0] * factors[2]}\)",
            $@"\({factors[1] * factors[2]}\)",
            $@"\({factors[1] * factors[3]}\)"
        }.Scramble();
        return new Question()
        {
            question   = @$"求 \({a}\) 與 \({b}\) 的最大公因數？",
            selections = selections.ToArray(),
            answer     = selections.IndexOf($@"\({answer}\)") + 1,
            explanation = $@"\({a}\) 的公因數是 \({MathG.GetFactors(a).Print()}\)" + "\n" +
                          $@"\({b}\) 的公因數是 \({MathG.GetFactors(b).Print()}\)" + "\n" +
                          $@"因此 \({a}\) 與 \({b}\) 的最大公因數是 \({answer}\)"
        };
    };

    [Question(difficulty: 1)]
    public Func<Question> _2 => () =>
    {
        var factors = new ListBuilder<int>([2, 3, 5]);
        var a = new ListBuilder<int>()
               .SetSeparator(@"\times")
               .SetQuote(@"\(", @"\)")
              + factors[0..1] * MathG.GetRandom(1, 1)
              + factors[1..2] * MathG.GetRandom(0, 2)
              + factors[2..3] * MathG.GetRandom(0, 1)
            ;
        var b = new ListBuilder<int>()
               .SetSeparator(@"\times")
               .SetQuote(@"\(", @"\)")
              + factors[0..1] * MathG.GetRandom(0, 1)
              + factors[1..2] * MathG.GetRandom(1, 1)
              + factors[2..3] * MathG.GetRandom(0, 2)
            ;
        var c = new ListBuilder<int>()
               .SetSeparator(@"\times")
               .SetQuote(@"\(", @"\)")
              + factors[0..1] * MathG.GetRandom(0, 2)
              + factors[1..2] * MathG.GetRandom(0, 1)
              + factors[2..3] * MathG.GetRandom(1, 1)
            ;

        var numA = (a + b);
        var numB = (a + c);
        var numC = (b + c);
        var answer = numA | numB | numC;
        var selections = new List<string>()
        {
            $"{(answer)}",
            $"{(answer + 2 + 3)}",
            $"{(answer + 3)}",
            $"{(answer - 2 + 5)}"
        }.Scramble();
        return new Question()
        {
            question   = $"{numA} 與 {numB} 及 {numC} 的最小公倍數是多少？",
            selections = selections.ToArray(),
            answer     = selections.IndexOf($"{answer}") + 1,
        };
    };


}