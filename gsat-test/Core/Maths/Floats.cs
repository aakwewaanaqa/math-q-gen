using System.Text.RegularExpressions;

namespace Gsat.Core.Maths;

public readonly struct Floats(int i, int shift)
{
    public readonly int i     = i;
    public readonly int shift = shift;

    public static Floats operator +(Floats a, int b)
    {
        var newI = a.i + b * 10 * a.shift;
        return new Floats(newI, a.shift).Simplify;
    }

    public static Floats operator +(Floats a, Floats b)
    {
        var shiftDelta = a.shift > b.shift ? a.shift - b.shift : b.shift - a.shift;
        var less       = a.shift > b.shift ? b : a;
        var more       = a.shift > b.shift ? a : b;
        var newI       = more.i + less.i * 10.Pow(shiftDelta);
        return new Floats(newI, more.shift).Simplify;
    }

    public static Floats operator -(Floats a, int b)
    {
        var newI = a.i - b * 10 * a.shift;
        return new Floats(newI, a.shift).Simplify;
    }

    public static Floats operator -(Floats a, Floats b)
    {
        var shiftDelta = a.shift > b.shift ? a.shift - b.shift : b.shift - a.shift;
        return a.shift > b.shift ?
            new Floats(a.i - b.i * 10.Pow(shiftDelta), a.shift).Simplify :
            new Floats(a.i * 10.Pow(shiftDelta) - b.i, a.shift).Simplify;
    }

    public static Floats operator *(Floats a, int b)
    {
        if (b == 0) return new Floats(0, 0);
        if (b == 1) return a;
        if (b.IsPowOf(10))
        {
            var newShift   = a.shift - b.Log(10);
            var newI       = a.i;
            if (b < 0) newI = -newI;
            return new Floats(newI, newShift).Simplify;
        }

        {
            var newI = a.i * b;
            return new Floats(newI, a.shift).Simplify;
        }
    }

    public static Floats operator *(Floats a, Floats b)
    {
        var newI     = a.i * b.i;
        var newShift = a.shift + b.shift;
        return new Floats(newI, newShift).Simplify;
    }

    public static Floats operator /(Floats a, int b)
    {
        if (b == 0) throw new DivideByZeroException("Divisor cannot be zero.");
        if (b == 1) return a;
        if (b.IsPowOf(10))
        {
            var newShift    = a.shift + b.Log(10);
            var newI        = a.i;
            if (b < 0) newI = -newI;
            return new Floats(newI, newShift).Simplify;
        }

        {
            var newI = a.i / b;
            return new Floats(newI, a.shift).Simplify;
        }
    }

    public static Floats operator /(Floats a, Floats b)
    {
        var maxShift = a.shift > b.shift ? a.shift : b.shift;
        var newI     = (a.i * maxShift) / (b.i * maxShift);
        return new Floats(newI, maxShift).Simplify;
    }

    public static bool operator >(Floats a, Floats b)
    {
        return (a - b).i > 0;
    }

    public static bool operator <(Floats a, Floats b)
    {
        return (a - b).i < 0;
    }

    public Floats Simplify
    {
        get
        {
            var newI     = i;
            var newShift = shift;

            if (i == 0) return new Floats(0, 0);

            while (newShift < 0)
            {
                newI *= 10;
                newShift++;
            }

            while (newI % 10 == 0 && newShift > 0)
            {
                newI /= 10;
                newShift--;
            }

            return new Floats(newI, newShift);
        }
    }

    public Floats Abs
    {
        get
        {
            var newI = i < 0 ? -i : i;
            return new Floats(newI, shift);
        }
    }

    public override string ToString()
    {
        try
        {
            if (i == 0) return "0";
            if (shift == 0) return i.ToString();

            var signInt                               = i < 0 ? -i : i;
            var signIntStr                            = signInt.ToString();
            if (signIntStr.Length < shift) signIntStr = new String('0', shift - signIntStr.Length) + signIntStr;
            signIntStr = signIntStr.Insert(signIntStr.Length - shift, ".");
            if (signIntStr[0] == '.') signIntStr = "0" + signIntStr;
            signIntStr = signIntStr.TrimEnd('0', '.');
            return i < 0 ? "-" + signIntStr : signIntStr;
        }
        catch (Exception e) { throw new Exception($"{i} {shift}", e); }
    }

    public string ToString(string format = "E")
    {
        switch (format)
        {
            case "E":
            {
                var str = $@"{i} \div {10.Pow(shift)}";
                return str;
            }
            default:
            {
                return base.ToString();
            }
        }
    }

    public Frac ToFrac()
    {
        var simple = Simplify;
        return new Frac(simple.i, 10.Pow(simple.shift)).Real;
    }
}