using Gsat.Core;

namespace Gsat.Structs;

public struct ListBuilder<T>
{
    public ListBuilder()
    {
    }

    private T[]    array           { get; init; } = [];
    private string stringSeparator { get; set; }  = ", ";
    private string stringBeginning { get; set; }  = "";
    private string stringEnding    { get; set; }  = "";

    public int Count => array.Length;

    private ListBuilder<T> Clone()
    {
        var result = new ListBuilder<T>
        {
            stringSeparator = stringSeparator,
            stringBeginning = stringBeginning,
            stringEnding    = stringEnding,
            array           = (T[])array.Clone()
        };
        return result;
    }

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

    public ListBuilder<T> Pick(IEnumerable<T> src, int index, int count)
    {
        var item = src.ElementAt(index);
        for (int i = 0; i < count; i++) array.Add(item);
        return this;
    }

    public static ListBuilder<T> operator +(ListBuilder<T> a, ListBuilder<T> b)
    {
        var result = a.Clone();
        result.array.AddRange(b.array);
        return result;
    }

    public static ListBuilder<T> operator +(ListBuilder<T> a, T b)
    {
        var result = a.Clone();
        result.array.Add(b);
        return result;
    }

    public static ListBuilder<T> operator -(ListBuilder<T> a, T b)
    {
        var result = a.Clone();
        result.array.Remove(b);
        return result;
    }

    public static ListBuilder<T> operator &(ListBuilder<T> a, ListBuilder<T> b)
    {
        var aList = a.array.ToList();
        var bList = b.array.ToList();
        return new ListBuilder<T>()
        {
            stringSeparator = a.stringSeparator,
            stringBeginning = a.stringBeginning,
            stringEnding    = a.stringEnding,
            array           = (from item in aList
                               let flag = bList.Remove(item)
                               where flag select item).ToArray()
        };
    }

    public static ListBuilder<T> operator |(ListBuilder<T> a, ListBuilder<T> b)
    {
        var aList = a.array.ToList();
        var bList = b.array.ToList();
        foreach (var item in aList) bList.Remove(item);
        var r     = aList.Concat(bList);
        return new ListBuilder<T>()
        {
            stringSeparator = a.stringSeparator,
            stringBeginning = a.stringBeginning,
            stringEnding    = a.stringEnding,
            array           = r.ToArray()
        };
    }

    public override string ToString()
    {
        return stringBeginning + string.Join(stringSeparator, array) + stringEnding;
    }
}