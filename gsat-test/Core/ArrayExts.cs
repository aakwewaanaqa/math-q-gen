namespace Gsat.Core;

public static class ArrayExts
{
    public static T[] AddRange<T>(this T[] array, IEnumerable<T> ienumerable)
    {
        int i   = array.Length;
        var src = ienumerable as T[] ?? ienumerable.ToArray();
        Array.Resize(ref array, array.Length + src.Length);
        for (; i < array.Length; i++) array[i] = src[i];
        return array;
    }

    public static T[] Add<T>(this T[] array, T item)
    {
        Array.Resize(ref array, array.Length + 1);
        array[^1] = item;
        return array;
    }

    public static T[] Remove<T>(this T[] array, T item)
    {
        var index = Array.IndexOf(array, item);
        if (index < 0) return array;
        for (int i = index; i < array.Length - 1; i++) array[i] = array[i + 1];
        Array.Resize(ref array, array.Length - 1);
        return array;
    }
}