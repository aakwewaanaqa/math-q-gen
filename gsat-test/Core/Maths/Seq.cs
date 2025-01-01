namespace Gsat.Core.Maths;

public partial struct Seq<T>()
{
    public Seq(string begin, string separator, string end) : this()
    {
        stringBeginning = begin;
        stringSeparator = separator;
        stringEnding    = end;
    }

    public Seq(IEnumerable<T> array) : this()
    {
        this.array = array.ToArray();
    }

    public Seq(IEnumerable<T> array, Seq<T> settings) : this()
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

    public int Count => array?.Length ?? 0;
    public Seq<T> this[Range r] => new(array[r], this);
    public T this[Index              i] => array[i];
    public T this[int                i] => array[i];

    public Seq<T> SetSeparator(string separator)
    {
        stringSeparator = separator;
        return this;
    }

    public Seq<T> SetQuote(string begin, string end)
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
}