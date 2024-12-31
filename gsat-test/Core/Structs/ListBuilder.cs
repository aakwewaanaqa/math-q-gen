namespace Gsat.Core.Structs;

public partial struct ListBuilder<T>()
{
    public ListBuilder(string begin, string separator, string end) : this()
    {
        stringBeginning = begin;
        stringSeparator = separator;
        stringEnding    = end;
    }

    public ListBuilder(IEnumerable<T> array) : this()
    {
        this.array = array.ToArray();
    }

    private ListBuilder(IEnumerable<T> array, ListBuilder<T> settings) : this()
    {
        this.array      = array.ToArray();
        stringSeparator = settings.stringSeparator;
        stringBeginning = settings.stringBeginning;
        stringEnding    = settings.stringEnding;
    }

    private T[]    array = [];
    private string stringSeparator { get; set; } = ", ";
    private string stringBeginning { get; set; } = "";
    private string stringEnding    { get; set; } = "";

    public int Count => array.Length;
    public ListBuilder<T> this[Range r] => new(array[r], this);
    public T this[Index              i] => array[i];
    public T this[int                i] => array[i];

    public ListBuilder<T> SetSeparator(string separator)
    {
        stringSeparator = separator;
        return this;
    }

    public ListBuilder<T> SetQuote(string begin, string end)
    {
        stringBeginning = begin;
        stringEnding    = end;
        return this;
    }

    public override string ToString()
    {
        return stringBeginning + string.Join(stringSeparator, array) + stringEnding;
    }

    public T Aggregate(Func<T, T, T> func)
    {
        return array.Aggregate(func);
    }

    public IEnumerable<U> Select<U>(Func<T, U> func)
    {
        return array.Select(item => func(item));
    }

    public T[] ToArray()
    {
        return (T[])array.Clone();
    }
}