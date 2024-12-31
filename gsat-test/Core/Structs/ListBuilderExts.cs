using static Gsat.Core.MathG;

namespace Gsat.Core.Structs;

public static class ListBuilderExts
{
    public static ListBuilder<T> ToBuilder<T>(this IEnumerable<T> ie)
    {
        return new ListBuilder<T>(ie);
    }
    
    public static ListBuilder<T> Choose<T>(this IEnumerable<T> ie, int count)
    {
        if (count <= 0) return new ListBuilder<T>([]);
        var array = ie.ToArray();
        var result = new T[count];
        for (var i = 0; i < count; i++) result[i] = array[GetRandom(0, array.Length)];
        return new ListBuilder<T>(result);
    }

    public static ListBuilder<T> Choose<T>(this IEnumerable<T> ie, int index, int count)
    {
        if (count <= 0) return new ListBuilder<T>([]);
        var array = ie.ToArray();
        var result = new T[count];
        for (var i = 0; i < count; i++) result[i] = array[GetRandom(0, array.Length)];
        return new ListBuilder<T>(result);
    }

    public static int Product(this ListBuilder<int> ints)
    {
        return ints.ToArray().Aggregate((a, b) => a * b);
    }
    
    
}