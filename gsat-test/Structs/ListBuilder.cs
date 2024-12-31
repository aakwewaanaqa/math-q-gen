using Gsat.Core;

namespace Gsat.Structs;

public struct ListBuilder<T>()
{
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
    public T this[Index i] => array[i];
    public T this[int i] => array[i];

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

    public static ListBuilder<T> operator +(ListBuilder<T> a, T[] b)
    {
        var array = new T[a.Count + b.Length];
        a.array.CopyTo(array, 0);
        b.CopyTo(array, a.Count);
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new ListBuilder<T>(array, a);
    }

    public static ListBuilder<T> operator +(ListBuilder<T> a, ListBuilder<T> b)
    {
        var array = new T[a.Count + b.Count];
        a.array.CopyTo(array, 0);
        b.array.CopyTo(array, a.Count);
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new ListBuilder<T>(array, a);
    }

    public static ListBuilder<T> operator +(ListBuilder<T> a, T b)
    {
        var array = new T[a.Count + 1];
        a.array.CopyTo(array, 0);
        array[^1] = b;
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new ListBuilder<T>(array, a);
    }

    public static ListBuilder<T> operator +(T b, ListBuilder<T> a)
    {
        var array = new T[a.Count + 1];
        array[0] = b;
        a.array.CopyTo(array, 1);
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new ListBuilder<T>(array, a);
    }

    public static ListBuilder<T> operator -(ListBuilder<T> a, T b)
    {
        var aList = a.array.ToList();
        aList.Remove(b);
        var array = aList.ToArray();
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new ListBuilder<T>(array, a);
    }

    public static ListBuilder<T> operator *(ListBuilder<T> a, int count)
    {
        if (count <= 0) return new ListBuilder<T>([], a);

        var array = new T[a.Count                               * count];
        for (var i = 0; i < count; i++) a.array.CopyTo(array, i * a.Count);
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new ListBuilder<T>(array, a);
    }

    public static ListBuilder<T> operator &(ListBuilder<T> a, ListBuilder<T> b)
    {
        var aList = a.array.ToList();
        var bList = b.array.ToList();
        var array = (from item in aList
                     let flag = bList.Remove(item)
                     where flag
                     select item).ToArray();
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new ListBuilder<T>()
        {
            stringSeparator = a.stringSeparator,
            stringBeginning = a.stringBeginning,
            stringEnding    = a.stringEnding,
            array           = array,
        };
    }

    public static ListBuilder<T> operator |(ListBuilder<T> a, ListBuilder<T> b)
    {
        var aList = a.array.ToList();
        var bList = b.array.ToList();
        foreach (var item in aList) bList.Remove(item);
        var array = aList.Concat(bList).ToArray();
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new ListBuilder<T>()
        {
            stringSeparator = a.stringSeparator,
            stringBeginning = a.stringBeginning,
            stringEnding    = a.stringEnding,
            array           = array
        };
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

    public ListBuilder<T> Choose(int count, bool canRepeat = false)
    {
        var list = new List<T>();
        for (var i = 0; i < count;)
        {
            var item = array[MathG.GetRandom(0, array.Length)];
            if (!canRepeat && list.Contains(item)) continue;
            list.Add(item);
            i++;
        }
        return new ListBuilder<T>(list, this);
    }
}