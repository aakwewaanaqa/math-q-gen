using Gsat.Structs;

namespace Gsat.Units;

/// <summary>
///     最大公因數與最小公倍數
/// </summary>
[QuestionMaker("math", "最大公因數與最小公倍數", 6)]
public class GcfAndLcm : IUnit
{
    [Question(2)]
    public Func<Question> _1 => () =>
    {
        var factors = new int[4];
        MathG.GetRandom(2, 10, factors);
        var a          = factors[0] * factors[1] * factors[2];
        var b          = factors[0] * factors[1] * factors[3];
        var answer     = factors[0] * factors[1];
        var selections = new List<string>()
        {
            $@"\({answer}\)",
            $@"\({answer * factors[0]}\)",
            $@"\({answer * factors[1]}\)",
            $@"\({answer * factors[2]}\)"
        }.Scramble();
        return new Question()
        {
            subject    = this.GetSubject(),
            unit       = this.GetUnit(),
            grade      = this.GetGrade(),
            question   = $"求 {a} 與 {b} 的最大公因數？",
            selections = selections.ToArray(),
            answer     = selections.IndexOf($@"\({answer}\)"),
        };
    };
}