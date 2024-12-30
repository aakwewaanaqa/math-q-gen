namespace Gsat.Structs;

public class ListBuilder<T>
{
    private List<T> list            { get; }      = [];
    private string  stringSeparator { get; set; } = ", ";
    private string  stringBeginning { get; set; } = "";
    private string  stringEnding    { get; set; } = "";

    public ListBuilder<T> SetSeparator(string separator)
    {
        stringSeparator = separator;
        return this;
    }

    public ListBuilder<T> SetQuote(string beginning, string ending)
    {
        stringBeginning = beginning;
        stringEnding    = ending;
        return this;
    }

    public ListBuilder<T> Clone()
    {
        var result = new ListBuilder<T>
        {
            stringSeparator = stringSeparator,
            stringBeginning = stringBeginning,
            stringEnding    = stringEnding
        };
        result.list.AddRange(list);
        return result;
    }

    public ListBuilder<T> Pick(IEnumerable<T> src, int index, int count)
    {
        for (int i = 0; i < count; i++) list.Add(src.ElementAt(index + i));
        return this;
    }

    public static ListBuilder<T> operator +(ListBuilder<T> a, ListBuilder<T> b)
    {
        var result = a.Clone();
        result.list.AddRange(b.list);
        return result;
    }

    public static ListBuilder<T> operator +(ListBuilder<T> a, T b)
    {
        var result = a.Clone();
        result.list.Add(b);
        return result;
    }

    public static ListBuilder<T> operator -(ListBuilder<T> a, T b)
    {
        var result = a.Clone();
        result.list.Remove(b);
        return result;
    }

    public static ListBuilder<T> operator &(ListBuilder<T> a, ListBuilder<T> b)
    {
        var result = new ListBuilder<T>()
        {
            stringSeparator = a.stringSeparator,
            stringBeginning = a.stringBeginning,
            stringEnding    = a.stringEnding
        };
        result.list.AddRange(a.list.Intersect(b.list));
        return result;
    }

    public static ListBuilder<T> operator |(ListBuilder<T> a, ListBuilder<T> b)
    {
        var result = new ListBuilder<T>()
        {
            stringSeparator = a.stringSeparator,
            stringBeginning = a.stringBeginning,
            stringEnding    = a.stringEnding
        };
        result.list.AddRange(a.list.Union(b.list));
        return result;
    }

    public override string ToString()
    {
        return stringBeginning + string.Join(stringSeparator, list) + stringEnding;
    }
}