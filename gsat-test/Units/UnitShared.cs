using System.Reflection;
using Gsat.Structs;

namespace Gsat.Units;

public static class UnitShared
{
    public static Question GetQuestion(this IUnit unit, int index)
    {
        var prop = unit.GetType().GetProperty($"_{index}")!;
        var q = ((Func<Question>)prop.GetValue(unit)!)();
        var qAtt = prop.GetCustomAttribute<QuestionAttribute>()!;
        var uAtt = unit.GetType().GetCustomAttribute<QuestionMakerAttribute>()!;
        q.difficulty = qAtt.Difficulty;
        q.subject    = uAtt.Subject;
        q.unitName   = uAtt.UnitName;
        q.grade      = uAtt.Grade;
        return q;
    }

    public static IList<string> Scramble(this IEnumerable<string> selections)
    {
        var list = selections.ToList();
        var result = new string[list.Count];
        list.CopyTo(result);
        for (var i = 0; i < list.Count; i++)
        {
            var j = MathG.GetRandom(0, list.Count);
            (result[i], result[j]) = (result[j], result[i]);
        }
        return result;
    }

    public static string Print(this IEnumerable<int> factors)
    {
        return string.Join(", ", factors);
    }
}