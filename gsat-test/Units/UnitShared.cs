using System.Reflection;
using Gsat.Structs;

namespace Gsat.Units;

public static class UnitShared
{
    public static string GetSubject(this IUnit unit)
    {
        return unit.GetType().GetCustomAttribute<QuestionMakerAttribute>().Subject;
    }

    public static string GetUnit(this IUnit unit)
    {
        return unit.GetType().GetCustomAttribute<QuestionMakerAttribute>().Unit;
    }

    public static int GetGrade(this IUnit unit)
    {
        return unit.GetType().GetCustomAttribute<QuestionMakerAttribute>().Grade;
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