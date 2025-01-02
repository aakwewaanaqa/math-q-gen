using System.Reflection;
using Gsat.Core;
using Gsat.Core.Interfaces;
using Gsat.Core.Structs;

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
}