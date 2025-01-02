using Gsat.Core.Structs;

namespace Gsat.Core.Maths;

public partial struct Seq<T>
{
    #region +
    public static Seq<T> operator +(Seq<T> a, IEnumerable<T> b)
    {
        var bArray = b.ToArray();
        var result = new T[a.Count + bArray.Length];
        a.array.CopyTo(result, 0);
        bArray.CopyTo(result, a.Count);
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(result);
        return new Seq<T>(result, a);
    }

    public static Seq<T> operator +(Seq<T> a, Seq<T> b)
    {
        var result = new T[a.Count + b.Count];
        a.array.CopyTo(result, 0);
        b.array.CopyTo(result, a.Count);
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(result);
        return new Seq<T>(result, a);
    }

    public static Seq<T> operator +(Seq<T> a, T b)
    {
        var array = new T[a.Count + 1];
        a.array.CopyTo(array, 0);
        array[^1] = b;
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new Seq<T>(array, a);
    }

    public static Seq<T> operator +(T b, Seq<T> a)
    {
        var array = new T[a.Count + 1];
        array[0] = b;
        a.array.CopyTo(array, 1);
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new Seq<T>(array, a);
    }

    /// <summary>
    ///     加入元素 T 到 Seq&lt;T&gt; 中，並重複加入 R 次
    /// </summary>
    public static Seq<T> operator +(Seq<T> a, (T, R) tuple)
    {
        var bCount = tuple.Item2.ToInt();
        var array = new T[a.Count + bCount];
        a.array.CopyTo(array, 0);
        for (var i = 0; i < bCount; i++) array[a.Count + i] = tuple.Item1;
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new Seq<T>(array, a);
    }
    #endregion

    #region >>

    /// <summary>
    ///     透過 C 來選 Seq&lt;T&gt; 的元素
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">C 所要取的數量大於 Seq 且不能重複時。</exception>
    public static Seq<T> operator >> (Seq<T> a, C b)
    {
        var list = new List<T>();
        var src  = a.ToSpan();

        if (src.Length < b.count && !b.canRepeat)
            throw new ArgumentOutOfRangeException(
                nameof(C.count),
                "C 的 count 大於 Seq 的長度，又不能重複元素！會導致無限迴圈。");

        if (!b.index.HasValue && !b.range.HasValue)
        {
            while (list.Count < b.count)
            {
                var pick = src[new R(0, src.Length)];
                if (!b.canRepeat && list.Contains(pick)) continue;
                list.Add(pick);
            }

            list.Sort();
            return new Seq<T>(list, a);
        }

        if (b.index.HasValue)
        {
            while (list.Count < b.count)
            {
                var pick = src[b.index.Value];
                list.Add(pick);
            }
            
            list.Sort();
            return new Seq<T>(list, a);
        }

        {
            var slice = src[b.range.Value];
            while (list.Count < b.count)
            {
                var pick = slice[new R(0, slice.Length)];
                if (!b.canRepeat && list.Contains(pick)) continue;
                list.Add(pick);
            }

            list.Sort();
            return new Seq<T>(list, a);
        }
    }

    public static Seq<Seq<T>> operator >>> (Seq<T> a, C c)
    {
        var items = a.ToList();
        int count = c.count.Value;
        var result = new List<List<T>>();
        GenerateCombinations(items, count, [], result);
        return result.Select(i => new Seq<T>(i, a)).ToSeq();

        void GenerateCombinations(List<T> items, int count, List<T> current, List<List<T>> result)
        {
            // 終止條件：選取數量達到需求
            if (current.Count == count)
            {
                result.Add([..current]);
                return;
            }

            // 終止條件：沒有更多元素可供選擇
            if (items.Count == 0)
            {
                return;
            }

            // 選擇第一個元素
            current.Add(items[0]);
            GenerateCombinations(items.GetRange(1, items.Count - 1), count, current, result);

            // 不選第一個元素
            current.RemoveAt(current.Count - 1);
            GenerateCombinations(items.GetRange(1, items.Count - 1), count, current, result);
        }
    }
    
    /// <summary>
    ///     透過 Func&lt;T, bool&gt; 來篩選 Seq&lt;T&gt; 的元素以 C 來選元素
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">C 所要取的數量大於 Seq 且不能重複時。</exception>
    public static Seq<T> operator >> (Seq<T> a, (C, Func<T, bool>) tuple)
    {
        var src = a.array.Where(i => tuple.Item2(i)).ToSeq();
        if (src.Count < tuple.Item1.count && !tuple.Item1.canRepeat)
            throw new ArgumentOutOfRangeException(
                nameof(C.count),
                "C 的 count 大於 Seq 的長度，又不能重複元素！會導致無限迴圈。");

        return src >> tuple.Item1;
    }
    
    /// <summary>
    ///     打亂 Seq&lt;T&gt; 的元素
    /// </summary>
    public static Seq<T> operator >>(Seq<T> seq, Scramble _)
    {
        var result = seq.ToList().OrderBy(_ => new R(0, seq.Count).ToInt());
        return new Seq<T>(result, seq);
    }
    #endregion

    public static Seq<T> operator -(Seq<T> a, T b)
    {
        var aList = a.array.ToList();
        aList.Remove(b);
        var array = aList.ToArray();
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new Seq<T>(array, a);
    }

    /// <summary>
    ///     重複 Seq&lt;T&gt; 的元素
    /// </summary>
    public static Seq<T> operator *(Seq<T> a, int count)
    {
        if (count <= 0) return new Seq<T>([], a);

        var array = new T[a.Count * count];
        for (var i = 0; i < count; i++) a.array.CopyTo(array, i * a.Count);
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new Seq<T>(array, a);
    }

    /// <summary>
    ///     交集
    /// </summary>
    public static Seq<T> operator &(Seq<T> a, Seq<T> b)
    {
        var aList = a.array.ToList();
        var bList = b.array.ToList();
        var array = (from item in aList
                     let flag = bList.Remove(item)
                     where flag
                     select item).ToArray();
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new Seq<T>
        {
            stringSeparator = a.stringSeparator,
            stringBeginning = a.stringBeginning,
            stringEnding    = a.stringEnding,
            array           = array
        };
    }

    /// <summary>
    ///     聯集
    /// </summary>
    public static Seq<T> operator |(Seq<T> a, Seq<T> b)
    {
        var aList = a.array.ToList();
        var bList = b.array.ToList();
        foreach (var item in aList) bList.Remove(item);
        var array = aList.Concat(bList).ToArray();
        if (typeof(T).IsAssignableTo(typeof(IComparable))) Array.Sort(array);
        return new Seq<T>
        {
            stringSeparator = a.stringSeparator,
            stringBeginning = a.stringBeginning,
            stringEnding    = a.stringEnding,
            array           = array
        };
    }
}