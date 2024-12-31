namespace Gsat.Core.Structs;

public struct Question
{
    public string   subject;     // 科目
    public string   unitName;    // 單元
    public int      grade;       // 年級
    public int      difficulty;  // 難度
    public string   question;    // 問題
    public string[] selections;  // 選項
    public int      answer;      // 答案
    public string   explanation; // 解析
}