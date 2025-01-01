using Gsat.Core.Maths;
using Gsat.Units;

namespace Gsat.Core.Structs;

public readonly struct QuestionBuilder(
    string[] questions,
    string   answer,
    string[] selections,
    string   explanation = "")
{
    public Question ToQuestion()
    {
        var q = questions[new R(0, questions.Length)];
        var s = (new Seq<string>(selections.Distinct()) >> new C(3)) +
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