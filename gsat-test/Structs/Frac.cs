namespace Gsat.Structs;

public readonly struct Frac(int n, int d)
{
    public int n { get; } = n;
    public int d { get; } = d;

    public static Frac operator +(Frac a, Frac b)
    {
        return new Frac(a.n * b.d + b.n * a.d, a.d * b.d).Reduce();
    }

    public static Frac operator -(Frac a, Frac b)
    {
        return new Frac(a.n * b.d - b.n * a.d, a.d * b.d).Reduce();
    }

    public Frac Reduce()
    {
        int gcd = MathG.Gcf(n, d);
        return new Frac(n / gcd, d / gcd);
    }

    public override string ToString()
    {
        return $"\\frac{{{n}}}{{{d}}}";
    }
}