namespace Gsat.Core.Maths;

public readonly struct RFloat(float min, float max, int precision = 1)
{
    public readonly float min = min;
    public readonly float max = max;
    public readonly int precision = precision;
    private static readonly Random random = new();

    public Floats ToFloat()
    {
        var f = random.NextDouble() * (max - min) + min;
        f *= Math.Pow(10, precision);
        return new Floats((int)Math.Round(f), precision);
    }

    public static implicit operator Floats(RFloat r) => r.ToFloat();

    public override string ToString()
    {
        return ToFloat().ToString();
    }
}