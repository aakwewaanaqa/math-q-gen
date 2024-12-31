using Gsat.Core;
using Gsat.Structs;

namespace Gsat.Units;

/// <summary>
///     最大公因數與最小公倍數
/// </summary>
[QuestionMaker("math", "最大公因數與最小公倍數", 6)]
public class GcfAndLcm : IUnit
{
    private static readonly ListBuilder<int> times =
        new ListBuilder<int>().SetSeparator(@"\times").SetQuote(@"\(", @"\)");

    private static readonly ListBuilder<int> commas =
        new ListBuilder<int>().SetSeparator(@", ").SetQuote(@"\(", @"\)");

    /// <summary>
    ///     求 64 與 30 的最大公因數？
    /// </summary>
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

    /// <summary>
    ///     2 * 2 * 3 與 2 * 3 * 5 及 3 * 5 * 7 的最小公倍數是多少？
    /// </summary>
    [Question(difficulty: 1)]
    public Func<Question> _2 => () =>
    {
        var factors = times + [2, 3, 5];
        var a = times
              + factors[0..1] * MathG.GetRandom(1, 1)
              + factors[1..2] * MathG.GetRandom(0, 2)
              + factors[2..3] * MathG.GetRandom(0, 1)
            ;
        var b = times
              + factors[0..1] * MathG.GetRandom(0, 1)
              + factors[1..2] * MathG.GetRandom(1, 1)
              + factors[2..3] * MathG.GetRandom(0, 2)
            ;
        var c = times
              + factors[0..1] * MathG.GetRandom(0, 2)
              + factors[1..2] * MathG.GetRandom(0, 1)
              + factors[2..3] * MathG.GetRandom(1, 1)
            ;

        var numA = (a + b);
        var numB = (a + c);
        var numC = (b + c);
        var lcm  = numA | numB | numC;
        new SelectionBuilder($"{lcm}", [
            $"{lcm + 2}",
            $"{lcm + 3}",
            $"{lcm + 5}",
            $"{lcm - 2}",
            $"{lcm - 3}",
            $"{lcm - 5}",
        ]).Output(out var selections, out var answer);

        return new Question()
        {
            question   = $"{numA} 與 {numB} 及 {numC} 的最小公倍數是多少？",
            selections = selections,
            answer     = answer,
        };
    };

    /// <summary>
    ///     42 / 🌵 = 🪴 .. 2，如果 🪴 比 1 大，🌵 最大是多少？
    /// </summary>
    [Question(difficulty: 1)]
    public Func<Question> _3 => () =>
    {
        var f = new ListBuilder<int>([2, 3, 4, 5, 6, 7]);
        var comp = (new ListBuilder<int>()
                       .SetSeparator(@"\times")
                       .SetQuote(@"\(", @"\)")
                  + f[0..1] * MathG.GetRandom(1, 2)
                  + f[1..2] * MathG.GetRandom(1, 2)
                  + f[2..3] * MathG.GetRandom(0, 2)
                  + f[3..4] * MathG.GetRandom(0, 2)
                  + f[5..6] * MathG.GetRandom(0, 2)
                   )
           .Aggregate((a, b) => a * b);
        var factors   = MathG.GetFactors(comp, withoutSelf: true).ToSortList();
        var divider   = factors.GetRandomRange(1, 12);
        var quotient  = comp / divider;
        var mod       = MathG.GetRandom(1, divider);
        var number    = divider * quotient + mod;
        var maxFactor = factors[^1];

        new SelectionBuilder(@$"\({maxFactor}\)", [
            @$"\({maxFactor + 1}\)",
            @$"\({maxFactor + 2}\)",
            @$"\({maxFactor + 3}\)",
            @$"\({maxFactor - 1}\)",
            @$"\({maxFactor - 2}\)",
            @$"\({maxFactor - 3}\)",
        ]).Output(out var selections, out var answer);

        return new Question()
        {
            question   = $@"\({number} \div \)🌵\( = \)🪴 \(\dots {mod} \)，如果 🪴 比 \(1\) 大，🌵 最大是多少？",
            answer     = answer,
            selections = selections,
            explanation = "如果要讓 🌵 最大就要讓 🪴 最小，因此 🌵 是最大因數。"  +
                          $@"先讓 \({number} - {mod}\) 就可以整除了，" +
                          $@"再找到 \({number}\) 的因數 \({factors.Print()}\) 中最大的是 \({maxFactor}\)。"
        };
    };

    /// 以下哪一個數不是 52 與 68 及 88 的公因數？
    [Question(difficulty: 1)]
    public Func<Question> _4 => () =>
    {
        var from       = commas + [2, 3, 4];
        var mult       = (commas + [4, 6, 7, 9, 11, 13]).Choose(3);
        var gcf        = from.Choose(MathG.GetRandom(2, 3), true);
        var a          = (gcf + mult[0]).Aggregate((a, b) => a * b);
        var b          = (gcf + mult[1]).Aggregate((a, b) => a * b);
        var c          = (gcf + mult[2]).Aggregate((a, b) => a * b);
        var factors    = MathG.GetFactors(gcf.Aggregate((a, b) => a * b));
        var whichIsNot = MathG.GetRandomExcept(2, 10, factors);
        new SelectionBuilder(
            $@"\({whichIsNot}\)",
            factors.Select(e => $@"\({e}\)").ToArray()
        ).Output(out var selections, out var answer);
        return new Question
        {
            question    = $@"以下哪一個數不是 \({a}\) 與 \({b}\) 及 \({c}\) 的公因數？",
            answer      = answer,
            selections  = selections,
            explanation = $@"\({gcf.ToArray().Print()}\)"
        };
    };
}