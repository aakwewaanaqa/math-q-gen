using Gsat.Core;
using Gsat.Core.Interfaces;
using Gsat.Core.Structs;

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
        var factors = new ListBuilder<int>([2, 3, 4, 5, 6, 7, 8, 9, 10]).Choose(4);
        var a       = factors[0] * factors[1] * factors[2];
        var b       = factors[0] * factors[1] * factors[3];
        var gcf     = factors[0] * factors[1];
        new SelectionBuilder(@$"\({gcf}\)", [
                @$"\({factors[0] * factors[2]}\)",
                @$"\({factors[0] * factors[3]}\)",
                @$"\({factors[1] * factors[2]}\)",
                @$"\({factors[1] * factors[3]}\)",
                @$"\({factors[2] * factors[3]}\)",
            ]
        ).Output(out var selections, out var answer);
        return new Question()
        {
            question   = @$"求 \({a}\) 與 \({b}\) 的最大公因數？",
            selections = selections,
            answer     = answer,
            explanation = $@"\({a}\) 的公因數是 \({MathG.GetFactors(a)}\)" + "\n" +
                          $@"\({b}\) 的公因數是 \({MathG.GetFactors(b)}\)" + "\n" +
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
        var a = times + factors[0]
                      + factors.Take(1, new R(0, 2))
                      + factors.Take(2, new R(0, 1));
        var b = times + factors[1]
                      + factors.Take(2, new R(0, 2))
                      + factors.Take(0, new R(0, 1));
        var c = times + factors[2]
                      + factors.Take(0, new R(0, 2))
                      + factors.Take(1, new R(0, 1));

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
        var comp = (times
                    + f.Take(0, new R(1, 2))
                    + f.Take(1, new R(1, 2))
                    + f.Take(2, new R(0, 2))
                    + f.Take(3, new R(0, 2))
                    + f.Take(4, new R(0, 2))
                    + f.Take(5, new R(0, 2))).Product();
        var factors   = MathG.GetFactors(comp, withoutSelf: true);
        var divider   = factors.GetFromR(new R(1, 12));
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
            explanation = "如果要讓 🌵 最大就要讓 🪴 最小，因此 🌵 是最大因數。" +
                          $@"先讓 \({number} - {mod}\) 就可以整除了，" +
                          $@"再找到 \({number}\) 的因數 \({factors}\) 中最大的是 \({maxFactor}\)。"
        };
    };

    /// 以下哪一個數不是 52 與 68 及 88 的公因數？
    [Question(difficulty: 1)]
    public Func<Question> _4 => () =>
    {
        var from       = commas + [2, 3, 4];
        var mult       = (commas + [4, 6, 7, 9, 11, 13]).Choose(3);
        var gcf        = from.Choose(new R(2, 3), true);
        var a          = (gcf + mult[0]).Product();
        var b          = (gcf + mult[1]).Product();
        var c          = (gcf + mult[2]).Product();
        var factors    = MathG.GetFactors(gcf.Product());
        var whichIsNot = new R(2, 10, factors);
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

    /// 找出 117 正確質因數分解
    [Question(difficulty: 1)]
    public Func<Question> _5 => () =>
    {
        var primes  = times + [2, 3];
        var a       = primes.Choose(4, true);
        var product = a.Product();
        new SelectionBuilder(a.ToString(), [
            $@"{a - a[0]}",
            $@"{a - a[1]}",
            $@"{a - a[2]}",
            $@"{a + a[0]}",
            $@"{a + a[1]}",
            $@"{a + a[2]}",
        ]).Output(out var selections, out var answer);
        return new Question
        {
            question    = $@"找出 \({product}\) 正確質因數分解",
            answer      = answer,
            selections  = selections,
            explanation = $@"\({product}\) 的質因數是 \({MathG.GetFactors(product)}\)"
        };
    };
}