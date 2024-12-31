namespace Gsat.Core.Structs;

public partial struct ListBuilder<T>
{
    public bool Contains(T item)
    {
        return array.Contains(item);
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
    
    public ListBuilder<T> Take(int index, int count)
    {
        var result = new T[count];
        Array.Fill(result, array[index]);
        return new ListBuilder<T>(result, this);
    }
}