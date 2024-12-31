namespace Gsat.Core.Structs;

public partial struct ListBuilder<T>
{
    #region +
    public static ListBuilder<T> operator +(ListBuilder<T> a, IEnumerable<T> b)
    {
        var bArray = b.ToArray();
        var result = new T[a.Count + bArray.Length];
        a.array.CopyTo(result, 0);
        bArray.CopyTo(result, a.Count);
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(result);
        return new ListBuilder<T>(result, a);
    }

    public static ListBuilder<T> operator +(ListBuilder<T> a, ListBuilder<T> b)
    {
        var result = new T[a.Count + b.Count];
        a.array.CopyTo(result, 0);
        b.array.CopyTo(result, a.Count);
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(result);
        return new ListBuilder<T>(result, a);
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
    #endregion

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

        var array = new T[a.Count * count];
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
        return new ListBuilder<T>
        {
            stringSeparator = a.stringSeparator,
            stringBeginning = a.stringBeginning,
            stringEnding    = a.stringEnding,
            array           = array
        };
    }

    public static ListBuilder<T> operator |(ListBuilder<T> a, ListBuilder<T> b)
    {
        var aList = a.array.ToList();
        var bList = b.array.ToList();
        foreach (var item in aList) bList.Remove(item);
        var array = aList.Concat(bList).ToArray();
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new ListBuilder<T>
        {
            stringSeparator = a.stringSeparator,
            stringBeginning = a.stringBeginning,
            stringEnding    = a.stringEnding,
            array           = array
        };
    }
}