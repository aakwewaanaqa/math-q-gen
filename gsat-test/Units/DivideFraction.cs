using Gsat.Core;
using Gsat.Core.Interfaces;
using Gsat.Core.Maths;
using Gsat.Core.Structs;

namespace Gsat.Units;

[QuestionMaker("math", "分數的除法", 6)]
public class DivideFraction : IUnit
{
    // 4 / 5/3 = 📦\frac{🚚}{5} 下列哪一個描述是正確的？
    [Question(1)]
    public Func<Question> _1 => () =>
    {
        int  a = 0,       n = 0;
        Frac b = default, x = default;
        while (x.i is null)
        {
            a = new R(2,  5);
            n = new R(10, 40);
            b = new Frac(n, n - new R(1, 5));
            x = (a / b).Real;
        }

        var answers = new Seq<string>(
        [
            $@"📦\(={x.i}\)",
            $@"🚚\(={x.n}\)",
        ]) >> new C(1);
        return new QuestionBuilder(
            [
                $$"""等式 \({{a}} \div {{b}} = 📦\frac{🚚}{{{x.d}}} \) 中，哪一個描述是正確的？""",
            ],
            answers[0],
            [
                $@"\(📦={x.n}\)",
                $@"\(📦={x.d}\)",
                $@"\(🚚={x.i}\)",
                $@"\(🚚={x.d}\)",
            ]
        ).ToQuestion();
    };

    [Question(2)]
    public Func<Question> _2 => () =>
    {
        int  total = 0;
        Frac a     = Frac.Zero, b = Frac.Zero;
        while (a == b || a + b > 1)
        {
            var f       = new Seq<int>([2, 3, 4]) >> new C(4,      canRepeat: true);
            var p       = new Seq<int>([3, 7, 11, 13]) >> new C(1, canRepeat: true);
            var factors = f + p;
            total = (factors).Product();
            var ad = (factors >> new C(2)).Product();
            var bd = (factors >> new C(2)).Product();
            a = new Frac(new R(1, ad), ad);
            b = new Frac(new R(1, bd), bd);
        }

        var fracDelta = (a - b).Abs;
        var delta     = fracDelta * total;
        var result    = total;

        var fracDeltaExp1 = a > b ? "國小 - 國中" : "國中 - 國小";
        var fracDeltaExp2 = a > b ? $"{a} - {b}" : $"{b} - {a}";
        return new QuestionBuilder(
            [
                $@"如果將一個工廠所生廠的牛奶，總產量🥛的 \({a}\) 分給國小；" +
                $@"總產量🥛的 \({b}\) 分給國中。" +
                $@"如果國小與國中的牛奶數量相差 \({delta}\)，" +
                $@"那麼這個工廠所生產的牛奶數量是多少呢？",
            ],
            $@"\({result}\)",
            [
                $@"\({(1 - fracDelta).Abs * total}\)",
                $@"\({(1 + fracDelta) * total}\)",
                $@"\({result - 1}\)",
                $@"\({result * 2}\)",
            ],
            // @$"因為相差的部分就是 \(全部 \times ({fracDeltaExp}) = {delta}\)，" + "\n" +
            // @$"因此 \(全部 \times {fracDelta} = {delta}\)，" + "\n" +
            // @$"又因為 \(全部 \times {fracDelta} \div {fracDelta} = {delta} \div {fracDelta}\)，" + "\n" +
            // @$"所以 \(全部 = {delta} \div {fracDelta}\)。"
            $$"""
              \begin{align*} 
              🥛 \times ({{fracDeltaExp1}}) &= {{delta}} \\
              🥛 \times ({{fracDeltaExp2}}) &= {{delta}} \\
              🥛 \times {{fracDelta}} &= {{delta}} \\
              🥛 \times {{fracDelta}} \div {{fracDelta}} &= {{delta}} \div {{fracDelta}} \\
              🥛 &= {{delta}} \div {{fracDelta}} \\
              🥛 &= {{delta}} \times {{fracDelta.Flip}} \\
              🥛 &= {{result}}
              \end{align*}
              """
        ).ToQuestion();
    };

    [Question(1)]
    public Func<Question> _3 => () =>
    {
        var a = new Frac(new R(20, 100), new R(20, 100));

        int d1 = new R(20, 100).ToInt();
        int d2 = new R(20, 100).ToInt();
        int d3 = new R(20, 100).ToInt();
        int d4 = new R(20, 100).ToInt();

        var f1 = new Frac(d1 - new R(2, 5).ToInt(), d1);
        var f2 = new Frac(d2 + new R(2, 5).ToInt(), d2);
        var f3 = new Frac(d3 + new R(2, 5).ToInt(), d3);
        var f4 = new Frac(d4 + new R(2, 5).ToInt(), d4);

        return new QuestionBuilder(
            [
                $@"下列哪一個算式的結果是大於 \({a}\) 的？",
            ],
            @$"\({a} \div {f1}\)",
            [
                @$"\({a} \div {f2}\)",
                @$"\({a} \div {f3}\)",
                @$"\({a} \div {f4}\)",
            ]
        ).ToQuestion();
    };

    [Question(1)]
    public Func<Question> _4 => () =>
    {
        var d      = new R(3, 14);
        var a      = new Frac(new R(3, 10), d);
        var b      = new Frac(new R(3, 10), d);
        var result = (a / b).Real;
        return new QuestionBuilder(
            [
                @$"\({a} \div {b} = ?\)"
            ],
            $@"\({result}\)",
            [
                $@"\({(a / b / 2).Real}\)",
                $@"\({(a / b * 2).Real}\)",
                $@"\({(a * b / 2).Real}\)",
                $@"\({(a * b * 2).Real}\)",
                $@"\({(a * b).Real}\)",
                $@"\({(a + b).Real}\)",
                $@"\({(a - b).Abs.Real}\)",
            ]
        ).ToQuestion();
    };
}