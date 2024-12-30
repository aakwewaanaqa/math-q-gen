namespace Gsat.Structs;

public static class MathG
{
    public static int GetRandom(int min, int max)
    {
        return new Random().Next(min, max);
    }

    public static void GetRandom(int min, int max, int[] result)
    {
        for (int i = 0; i < result.Length; )
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
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }

    public static IEnumerable<int> GetFactors(int a)
    {
        for (var i = 1; i <= a; i++)
        {
            if (a % i == 0)
                yield return i;
        }
    }

    public static IEnumerable<int> GetCommonFactors(int a, int b)
    {
        var factorsA = GetFactors(a).ToList();
        var factorsB = GetFactors(b).ToList();
        return factorsA.Intersect(factorsB);
    }
}