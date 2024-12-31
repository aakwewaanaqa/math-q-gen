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

    public static int GetRandomExcept(int min, int max, IEnumerable<int> except)
    {
        var range = Enumerable.Range(min, max - min).Except(except).ToArray();
        return range[random.Next(0, range.Length)];
    }

    public static void GetRandom(int min, int max, int[] result)
    {
        for (int i = 0; i < result.Length;)
        {
            var r = GetRandom(min, max);
            if (result.Contains(r)) continue;
            result[i] = r;
            i++;
        }
    }

    public static int Gcf(int a, int b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b) a %= b;
            else b       %= a;
        }

        return a | b;
    }

    public static IEnumerable<int> GetFactors(int a, bool withoutSelf = false)
    {
        for (var i = 1; i <= a; i++)
        {
            if (withoutSelf && i == a) continue;
            if (a % i == 0) yield return i;
        }
    }

    public static IEnumerable<int> GetCommonFactors(int a, int b)
    {
        var factorsA = GetFactors(a).ToList();
        var factorsB = GetFactors(b).ToList();
        return factorsA.Intersect(factorsB);
    }

    public static IList<T> ToSortList<T>(this IEnumerable<T> ie)
    {
        var list = ie.ToList();
        list.Sort();
        return list;
    }

    public static T GetRandom<T>(this IEnumerable<T> ie)
    {
        return ie.ElementAt(random.Next(0, ie.Count()));
    }

    public static int GetRandomRange(this IEnumerable<int> ie, int min, int max)
    {
        return ie.Where(i => i >= min && i <= max).GetRandom();
    }
}