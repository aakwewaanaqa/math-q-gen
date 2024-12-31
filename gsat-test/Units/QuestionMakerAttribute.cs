namespace Gsat.Units;

public class QuestionMakerAttribute(string subject, string unitName, int grade) : Attribute
{
    public string Subject   { get; set; } = subject;
    public string UnitName  { get; set; } = unitName;
    public int    Grade    { get; set; } = grade;
}