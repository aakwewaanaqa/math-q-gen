namespace Gsat.Units;

public class QuestionMakerAttribute(string subject, string unit, int grade) : Attribute
{
    public string Subject { get; set; } = subject;
    public string Unit    { get; set; } = unit;
    public int    Grade   { get; set; } = grade;
}