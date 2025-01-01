using Gsat.Core.Structs;

namespace Gsat.Core.Maths;

/// <summary>
///     Choose 的數學運算
/// </summary>
public readonly struct C
{
    public readonly Index? index;
    public readonly Range? range;
    public readonly int?   count;
    public readonly bool   canRepeat;

    public C(int count = 1, bool canRepeat = false)
    {
        this.count     = count;
        this.canRepeat = canRepeat;
    }

    public C(Index index, int count = 1)
    {
        this.index     = index;
        this.count     = count;
    }

    public C(Range range, int count = 1, bool canRepeat = false)
    {
        this.range     = range;
        this.count     = count;
        this.canRepeat = canRepeat;
    }
}