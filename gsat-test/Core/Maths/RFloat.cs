namespace Gsat.Core.Maths;

public readonly struct RFloat(float min, float max, int precision = 1)
{
    public readonly float min = min;
    public readonly float max = max;
    public readonly int precision = precision;
    private static readonly Random random = new();

    public float ToFloat()
    {
        return (float) Math.Round(random.NextDouble() * (max - min) + min, precision);
    }

    public static implicit operator float(RFloat r) => r.ToFloat();
}