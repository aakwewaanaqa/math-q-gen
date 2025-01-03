using Gsat.Core;
using Gsat.Core.Interfaces;
using Gsat.Core.Maths;
using Gsat.Core.Structs;

namespace Gsat.Units;

[QuestionMaker("math", "三位以下小數的除法", 6)]
public class DivideFloating : IUnit
{
    // big / small = ?
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

    /// x / a / b = ?
    [Question(1)]
    public Func<Question> _2 => () =>
    {
        var o = (new Seq<int>([2, 3, 4]) >> new C(3, canRepeat: true)).Product();
        var a = new RFloat(1, 5, 1).ToFloat();
        var b = new RFloat(1, 5, 1).ToFloat();
        var x = a * b * o;

        {
            var abShifts         = 10.Pow(a.shift) * 10.Pow(b.shift);
            var xInt             = x * abShifts;
            var aInt             = a.i;
            var bInt             = b.i;
            var xDivA            = xInt / aInt;
            var xDivADivB        = xDivA / bInt;
            var shiftsExpression = $@"\div {abShifts} \times {abShifts}";

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
                ],
                $$"""
                  \begin{align*}
                  {{x}} \div {{a}} \div {{b}} &= ? && 增加括分的 {{shiftsExpression}} \\
                  {{xInt}} \div {{aInt}} \div {{bInt}} {{shiftsExpression}} &= ? && 先除以 {{aInt}} \\
                  {{xDivA}} \div {{bInt}} {{shiftsExpression}} &= ? && 再除以 {{bInt}} \\
                  {{xDivADivB}} {{shiftsExpression}} &= ? && 得到答案 \\
                  {{o}} &= ? && \\
                  \end{align*}
                  """
            ).ToQuestion();
        }
    };

    /// 下列哪一個計算式是正確的？
    [Question(2)]
    public Func<Question> _3 => () =>
    {
        const int COUNT       = 4;
        var       commonFs    = new Seq<int>([2, 3, 4]);
        var       expressions = new string[COUNT];
        for (int i = 0; i < COUNT; i++)
        {
            var fFactors =
                (Def.CommonFactors >> new C(5, canRepeat: true)) +
                (Def.CommonPrimes >> new C(1));

            var iFactors =
                fFactors >> new C(2);

            var f                       = new Floats(fFactors.Product(), 2);
            var j                       = iFactors.Product();
            var x                       = (f / j).Simplify;
            if (i != 0 && i % 2 == 1) x *= (commonFs >> new C(1))[0];
            if (i != 0 && i % 2 == 0) x /= (commonFs >> new C(1))[0];
            expressions[i] = $@"\({f} \div {j} = {x}\)";
        }

        return new QuestionBuilder(
            [
                "下列哪一個計算式是正確的？"
            ], expressions[0],
            expressions[1..]
        ).ToQuestion();
    };

    /// 請問對於等式 f1 / divider □ f2 / divider， □ 應該填入什麼才正確呢？
    [Question(1)]
    public Func<Question> _4 => () =>
    {
        var f1          = new RFloat(1, 7, 2).ToFloat();
        var f2          = new RFloat(1, 7, 2).ToFloat();
        var divider     = new RFloat(1, 7, 2).ToFloat();
        var expressions = $@"\({f1} \div {divider} □ {f2} \div {divider}\)";
        var answer      = f1 > f2 ? @"\gt" : @"\lt";
        return new QuestionBuilder(
            [
                @$"請問對於不等式 ({expressions}) 中， \(□\) 應該填入什麼才正確呢？"
            ],
            $@"\({answer}\)",
            [
                @"\(\gt\)",
                @"\(\lt\)",
                @"\(=\)",
            ],
            @"因為不等式兩邊的表達式的除數一樣大，所以被除數越大，那麼結果也會越大。" + "\n" +
            @"也可以使用以下等量公理去解開等式的結果：" + "\n" +
            $$"""
              \begin{align*}
              {{f1}} \div {{divider}} &□ {{f2}} \div {{divider}} \\
              {{f1}} \div {{divider}} \times {{divider}} &□ {{f2}} \div {{divider}} \times {{divider}} \\
              {{f1}} \times 1 &□ {{f2}} \times 1 \\
              {{f1}} &{{answer}} {{f2}} \\
              \end{align*}
              """
        ).ToQuestion(3);
    };

    /// 請問對於等式 f1 / divider □ f2 / divider， □ 應該填入什麼才正確呢？
    [Question(1)]
    public Func<Question> _5 => () =>
    {
        var f1          = new RFloat(0, 1, 2).ToFloat();
        var f2          = new RFloat(0, 1, 2).ToFloat();
        var number      = new RFloat(1, 7, 2).ToFloat();
        var expressions = $@"\({number} \div {f1} □ {number} \div {f2}\)";
        var answer      = f1 < f2 ? @"\gt" : @"\lt";

        {
            var frac1         = new Frac(10.Pow(f1.shift), f1.i);
            var frac2         = new Frac(10.Pow(f2.shift), f2.i);
            var lcm           = MathG.Lcm(frac1.d, frac2.d);
            var expandedFrac1 = frac1.Expand(lcm / frac1.d);
            var expandedFrac2 = frac2.Expand(lcm / frac2.d);

            return new QuestionBuilder(
                [
                    @$"請問對於不等式 ({expressions}) 中， \(□\) 應該填入什麼才正確呢？"
                ],
                $@"\({answer}\)",
                [
                    @"\(\gt\)",
                    @"\(\lt\)",
                    @"\(=\)",
                ],
                @"因為不等式兩邊的表達式的被除數一樣大，又因為除數\(\lt1\)，所以除數越小結果越大。" + "\n" +
                @"也可以使用以下等量公理去解開等式的結果：" + "\n" +
                $$"""
                  \begin{align*}
                  {{number}} \div {{f1}} &□ {{number}} \div {{f2}} \\
                  {{number}} \div {{f1}} \div {{number}} &□ {{number}} \div {{f2}} \div {{number}} \\
                  \frac{{{1}}}{{{f1}}} &□ \frac{{{1}}}{{{f2}}} \\
                  {{frac1}} &□ {{frac2}} \\
                  {{expandedFrac1}} &{{answer}} {{expandedFrac2}} \\
                  \end{align*}
                  """
            ).ToQuestion(3);
        }
    };
}