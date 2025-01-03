namespace Gsat.Core.Maths;

public readonly partial struct Frac(int n, int d, int? i = null)
{
    public readonly int? i = i == 0 ? null : i;
    public readonly int  n = n;
    public readonly int  d = d;

    public static Frac Zero => new Frac(0, 1);

    public bool IsFake => n > d;

    public Frac Expand(int m)
    {
        var newN = n * m;
        var newD = d * m;
        return new Frac(newN, newD);
    }

    public Frac Abs
    {
        get
        {
            var newN = Math.Abs(Fake.n);
            return new Frac(newN, d);
        }
    }

    public Frac Real
    {
        get
        {
            if (n % d == 0) return new Frac(n / d, 1);
            var gcf  = MathG.Gcf(n % d, d);
            var newN = n / gcf;
            var newD = d / gcf;
            var newI = i ?? 0;
            if (newN > newD)
            {
                newI += newN / newD;
                newN %= newD;
            }
            return new Frac(newN, newD, newI);
        }
    }

    public Frac Fake
    {
        get
        {
            var newN = (i ?? 0 * d) + n;
            return new Frac(newN, d);
        }
    }

    public Frac Flip => new Frac(d, n);

    public override string ToString()
    {
        if (d == 1) return @$"{n}";
        if (i != null) return @$"{i}\frac{{{n}}}{{{d}}}";
        return @$"\frac{{{n}}}{{{d}}}";
    }

    public float ToFloat() => (float)Fake.n / d;
}