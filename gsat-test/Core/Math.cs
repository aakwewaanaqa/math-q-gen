using Gsat.Core.Maths;
using Gsat.Core.Structs;
using static System.Random;

namespace Gsat.Core;

public static class MathG
{
    private static readonly Random random = new();

    public static readonly int[] Primes =
    [
        2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107,
        109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229,
        233, 239, 241, 251, 257, 263, 269, 271
    ];

    public static int GetRandom(int min, int max, bool isPrime = false)
    {
        if (!isPrime) return random.Next(min, max);
        var primes = Primes.Where(p => p >= min && p <= max).ToArray();
        return primes[random.Next(0, primes.Length)];
    }

    public static int Gcf(int a, int b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b) a %= b;
            else b       %= a;
        }

        return a == 0 ? b : a;
    }

    public static int Gcm(int a, int b) => a * b / Gcf(a, b);

    public static Seq<int> GetFactors(int a, bool withoutSelf = false)
    {
        var list = new List<int>();
        for (var i = 1; i <= a; i++)
        {
            if (withoutSelf && i == a) continue;
            if (a % i == 0) list.Add(i);
        }

        return new Seq<int>(list);
    }

    public static int Digits(this int a)
    {
        a = a < 0 ? -a : a;
        return a.ToString().Length;
    }

    public static int Pow(this int a, int p)
    {
        if (p < 0) throw new Exception("Power must be non-negative.");
        if (p == 0) return 1;
        for (int i = 0; i < p; i++) a *= a;
        return a;
    }

    public static int Log(this int a, int @base)
    {
        if (a < 0 || @base < 0) throw new Exception("Both base and number must be non-negative.");
        if (a == 0) return 0;
        if (a == 1) return 1;
        var i = 0;
        while (a > 1)
        {
            a /= @base;
            i++;
        }

        return i;
    }
}