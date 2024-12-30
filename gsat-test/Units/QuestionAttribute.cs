namespace Gsat.Units;

public class QuestionAttribute(int difficulty) : Attribute
{
    public int Difficulty { get; set; } = difficulty;
}