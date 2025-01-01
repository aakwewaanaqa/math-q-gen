using Gsat.Core;
using Gsat.Core.Interfaces;
using Gsat.Core.Maths;
using Gsat.Core.Structs;

namespace Gsat.Units;

/// <summary>
///     最大公因數與最小公倍數
/// </summary>
[QuestionMaker("math", "最大公因數與最小公倍數", 6)]
public class GcfAndLcm : IUnit
{
    private static readonly Seq<int> times =
        new Seq<int>().SetSeparator(@"\times").SetQuote(@"\(", @"\)");

    private static readonly Seq<int> commas =
        new Seq<int>().SetSeparator(@", ").SetQuote(@"\(", @"\)");

    /// <summary>
    ///     求 64 與 30 的最大公因數？
    /// </summary>
    [Question(difficulty: 1)]
    public Func<Question> _1 => () =>
    {
        var f   = new Seq<int>([2, 3, 4, 5, 6, 7, 8, 9, 10]) >> new C(4); // 因數乘積
        var gcf = f[0] * f[1];                                           // 公因數
        var a   = f[0] * f[1] * f[2];
        var b   = f[0] * f[1] * f[3];
        return new QuestionBuilder(
            [
                @$"求 \({a}\) 與 \({b}\) 的最大公因數？",
                @$"求 \(({a}, {b}) =?\)",
            ],
            @$"\({gcf}\)",
            [
                @$"\({f[0] * f[2]}\)",
                @$"\({f[0] * f[3]}\)",
                @$"\({f[1] * f[2]}\)",
                @$"\({f[1] * f[3]}\)",
                @$"\({f[2] * f[3]}\)",
            ],
            $@"\({a}\) 的公因數是 \({MathG.GetFactors(a)}\)" + "\n" +
            $@"\({b}\) 的公因數是 \({MathG.GetFactors(b)}\)" + "\n" +
            $@"因此 \({a}\) 與 \({b}\) 的最大公因數是 \({gcf}\)"
        ).ToQuestion();
    };

    /// <summary>
    ///     2 * 2 * 3 與 2 * 3 * 5 及 3 * 5 * 7 的最小公倍數是多少？
    /// </summary>
    [Question(difficulty: 1)]
    public Func<Question> _2 => () =>
    {
        var factors = times + [2, 3, 5];
        var a = times + factors[0]
                      + (factors >> new C(1, new R(0, 2)))
                      + (factors >> new C(2, new R(0, 1)));
        var b = times + factors[1]
                      + (factors >> new C(2, new R(0, 2)))
                      + (factors >> new C(0, new R(0, 1)));
        var c = times + factors[2]
                      + (factors >> new C(0, new R(0, 2)))
                      + (factors >> new C(1, new R(0, 1)));

        var numA = (a + b);
        var numB = (a + c);
        var numC = (b + c);
        var lcm  = numA | numB | numC;
        return new QuestionBuilder(
            [
                @$"{numA} 與 {numB} 及 {numC} 的最小公倍數是多少？",
            ],
            $"{lcm}",
            [
                $"{lcm + 2}",
                $"{lcm + 3}",
                $"{lcm + 5}",
                $"{lcm - 2}",
                $"{lcm - 3}",
                $"{lcm - 5}",
            ]).ToQuestion();
    };

    /// <summary>
    ///     42 / 🌵 = 🪴 .. 2，如果 🪴 比 1 大，🌵 最大是多少？
    /// </summary>
    [Question(difficulty: 1)]
    public Func<Question> _3 => () =>
    {
        var comp = (times
                    + (2, new R(1, 2))
                    + (3, new R(1, 2))
                    + (4, new R(0, 2))
                    + (5, new R(0, 2))
                    + (6, new R(0, 2))
                    + (7, new R(0, 2))).Product();
        var factors   = MathG.GetFactors(comp, withoutSelf: true);
        var divider   = (factors >> (new C(1), f => f is >= 1 and <= 10))[0];
        var quotient  = comp / divider;
        var mod       = new R(1, divider);
        var number    = divider * quotient + mod;
        var maxFactor = factors[^1];

        return new QuestionBuilder(
            [
                $@"\({number} \div \)🌵\( = \)🪴 \(\dots {mod} \)，如果 🪴 比 \(1\) 大，🌵 最大是多少？",
            ],
            @$"\({maxFactor}\)",
            [
                @$"\({maxFactor + 1}\)",
                @$"\({maxFactor + 2}\)",
                @$"\({maxFactor + 3}\)",
                @$"\({maxFactor - 1}\)",
                @$"\({maxFactor - 2}\)",
                @$"\({maxFactor - 3}\)",
            ]).ToQuestion();
    };

    /// 以下哪一個數不是 52 與 68 及 88 的公因數？
    [Question(difficulty: 1)]
    public Func<Question> _4 => () =>
    {
        var from       = commas + [2, 3, 4];
        var mult       = commas + [4, 6, 7, 9, 11, 13] >> new C(3);
        var gcf        = from >> new C(new R(2, 3), true);
        var a          = (gcf + mult[0]).Product();
        var b          = (gcf + mult[1]).Product();
        var c          = (gcf + mult[2]).Product();
        var factors    = MathG.GetFactors(gcf.Product());
        var whichIsNot = new R(2, 10, factors);
        return new QuestionBuilder(
            [
                $@"以下哪一個數不是 \({a}\\) 與 \({b}\) 及 \({c}\) 的公因數？\",
            ],
            $@"\({whichIsNot}\)",
            factors.Select(e => $@"\({e}\)").ToArray()
        ).ToQuestion();
    };

    /// 找出 117 正確質因數分解
    [Question(difficulty: 1)]
    public Func<Question> _5 => () =>
    {
        var primes  = times + [2, 3];
        var a       = primes >> new C(4, true);
        var product = a.Product();
        return new QuestionBuilder(
            [
                $@"找出 \({product}\) 正確質因數分解",
            ],
            a.ToString(),
            [
                $@"{a - a[0]}",
                $@"{a - a[1]}",
                $@"{a - a[2]}",
                $@"{a + a[0]}",
                $@"{a + a[1]}",
                $@"{a + a[2]}",
            ]).ToQuestion();
    };
}