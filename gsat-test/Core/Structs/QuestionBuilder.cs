using System.Text;
using Gsat.Core.Maths;
using Gsat.Units;

namespace Gsat.Core.Structs;

public readonly struct QuestionBuilder(
    string[] questions,
    string   answer,
    string[] selections,
    string   explanation = "")
{
    public Question ToQuestion(int selectionCount = 4)
    {
        var q             = questions[new R(0, questions.Length)];
        var selectionList = selections.Distinct().ToList();
        selectionList.Remove(answer);

        if (selectionList.Count < (selectionCount - 1))
        {
            var log = selections.Aggregate((a, b) => $"{a}\n    {b}");
            throw new Exception("selection less than 3: \n" + log);
        }

        var s = (new Seq<string>(selectionList) >> new C((selectionCount - 1))) +
                answer >> 
                new Scramble();
        return new Question
        {
            question    = q,
            selections  = s.ToArray(),
            answer      = s.IndexOf(answer) + 1,
            explanation = explanation
        };
    }
}