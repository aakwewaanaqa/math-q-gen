using Gsat.Core.Interfaces;

namespace Gsat.Core.Structs;

public readonly struct R(int min, int max) : IRandom<int>
{
    private static readonly Random random = new();
    
    public int GetValue() => random.Next(min, max);
    
    public static implicit operator int(R r) => r.GetValue();
}