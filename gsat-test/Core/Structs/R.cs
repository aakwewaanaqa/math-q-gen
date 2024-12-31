using Gsat.Core.Interfaces;

namespace Gsat.Core.Structs;

public readonly struct R(int min, int max) : IRandom<int>
{
    public readonly         int    min    = min;
    public readonly         int    max    = max;
    private static readonly Random random = new();

    private ListBuilder<int> except { get; }

    public R(int min, int max, ListBuilder<int> except) : this(min, max)
    {
        this.except = except;
    }

    public int GetValue()
    {
        if (except.Count == 0) return random.Next(min, max);
        var value                            = random.Next(min, max);
        while (except.Contains(value)) value = random.Next(min, max);
        return random.Next(min, max);
    }

    public static implicit operator int(R r) => r.GetValue();
}