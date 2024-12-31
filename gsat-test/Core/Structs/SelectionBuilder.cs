using Gsat.Units;

namespace Gsat.Core.Structs;

public readonly struct SelectionBuilder(string answer, string[] selections)
{
    private readonly string   answer     = answer;
    private readonly string[] selections = selections;

    public void Output(out string[] selections, out int answer)
    {
        var list = this.selections.ToList();
        while (list.Count > 3)
        {
            var r = MathG.GetRandom(0, list.Count);
            list.RemoveAt(r);
        }

        list.Add(this.answer);

        selections = list.Scramble().ToArray();
        answer     = Array.IndexOf(selections, this.answer) + 1;
    }
}