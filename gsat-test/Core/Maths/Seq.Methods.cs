namespace Gsat.Core.Maths;

public partial struct Seq<T>
{
    public bool Contains(T item)
    {
        return array.Contains(item);
    }

    public int IndexOf(T item)
    {
        return Array.IndexOf(array, item);
    }
    
    public Span<T> ToSpan()
    {
        return array;
    }

    public List<T> ToList()
    {
        return array.ToList();
    }

    public T[] ToArray()
    {
        return (T[])array.Clone();
    }
}