using Gsat.Core.Structs;
using static Gsat.Core.MathG;

namespace Gsat.Core.Maths;

public static class SeqExts
{
    public static Seq<T> ToSeq<T>(this IEnumerable<T> ie)
    {
        return new Seq<T>(ie);
    }
    
    public static Seq<T> Choose<T>(this IEnumerable<T> ie, int count)
    {
        if (count <= 0) return new Seq<T>([]);
        var array = ie.ToArray();
        var result = new T[count];
        for (var i = 0; i < count; i++) result[i] = array[GetRandom(0, array.Length)];
        return new Seq<T>(result);
    }

    public static Seq<T> Choose<T>(this IEnumerable<T> ie, int index, int count)
    {
        if (count <= 0) return new Seq<T>([]);
        var array = ie.ToArray();
        var result = new T[count];
        for (var i = 0; i < count; i++) result[i] = array[GetRandom(0, array.Length)];
        return new Seq<T>(result);
    }

    public static int Product(this Seq<int> ints)
    {
        return ints.ToArray().Aggregate((a, b) => a * b);
    }
    
    public static int GetFromR(this Seq<int> ints, R r)
    {
        var where = ints.ToArray().Where(i => i >= r.min && i <= r.max).ToArray();
        return where.ElementAt(GetRandom(0, where.Length));
    }
}