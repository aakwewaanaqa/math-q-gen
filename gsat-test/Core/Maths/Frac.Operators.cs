namespace Gsat.Core.Maths;

public readonly partial struct Frac
{
    public static bool operator ==(Frac a, Frac b)
    {
        var aReal = a.Real;
        var bReal = b.Real;
        return aReal.n == bReal.n && aReal.d == bReal.d && aReal.i == bReal.i;
    }

    public static bool operator !=(Frac a, Frac b)
    {
        return !(a == b);
    }

    public static bool operator >(Frac a, Frac b)
    {
        return a.ToFloat() > b.ToFloat();
    }

    public static bool operator <(Frac a, Frac b)
    {
        return a.ToFloat() < b.ToFloat();
    }

    public static bool operator <(Frac a, int b)
    {
        return a.ToFloat() < b;
    }

    public static bool operator >(Frac a, int b)
    {
        return a.ToFloat() < b;
    }

    public static Frac operator +(Frac a, Frac b)
    {
        var gcm  = MathG.Lcm(a.d, b.d);
        var newN = a.Fake.n * (gcm / a.d) + b.Fake.n * (gcm / b.d);
        return new Frac(newN, gcm);
    }

    public static Frac operator +(Frac a, int b)
    {
        var newN = a.n + a.d * b;
        return new Frac(newN, a.d);
    }

    public static Frac operator +(int b, Frac a)
    {
        var newN = a.n + a.d * b;
        return new Frac(newN, a.d);
    }

    public static Frac operator -(Frac a, int b)
    {
        var newN = a.n - a.d * b;
        return new Frac(newN, a.d);
    }

    public static Frac operator -(int b, Frac a)
    {
        var newN = a.n - a.d * b;
        return new Frac(newN, a.d);
    }

    public static Frac operator -(Frac a, Frac b)
    {
        var gcm  = MathG.Lcm(a.d, b.d);
        var newN = a.Fake.n * (gcm / a.d) - b.Fake.n * (gcm / b.d);
        return new Frac(newN, gcm);
    }

    public static Frac operator *(Frac a, Frac b)
    {
        var newN = a.Fake.n * b.Fake.n;
        var newD = a.Fake.d * b.Fake.d;
        return new Frac(newN, newD);
    }

    public static Frac operator /(Frac a, Frac b)
    {
        var newN = a.Fake.n * b.d;
        var newD = b.Fake.n * a.d;
        return new Frac(newN, newD);
    }

    public static Frac operator /(int a, Frac b)
    {
        var bn = (b.i ?? 0 * b.d) + b.n;
        return new Frac(a * b.d, bn);
    }

    public static Frac operator /(Frac b, int a)
    {
        var newD = b.d * a;
        return new Frac(b.n, newD);
    }

    public static Frac operator *(Frac a, int b)
    {
        var fakeA = a.Fake;
        return new Frac(fakeA.n * b, fakeA.d).Real;
    }
}