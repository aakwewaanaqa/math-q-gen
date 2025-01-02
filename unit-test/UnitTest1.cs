using Gsat.Core.Maths;
using Gsat.Core.Structs;

namespace unit_test;

public class Tests
{
    [Test]
    public void TestFloatsDisplay()
    {
        Assert.Multiple(() =>
        {
            Assert.That(new Floats(123, 2).ToString(), Is.EqualTo("1.23"));
            Assert.That(new Floats(123, 3).ToString(), Is.EqualTo("0.123"));
            Assert.That(new Floats(123, 4).ToString(), Is.EqualTo("0.0123"));

            Assert.That(new Floats(-123, 2).ToString(), Is.EqualTo("-1.23"));
            Assert.That(new Floats(-123, 3).ToString(), Is.EqualTo("-0.123"));
            Assert.That(new Floats(-123, 4).ToString(), Is.EqualTo("-0.0123"));
        });
    }

    [Test]
    public void Test1()
    {
        var seq = new Seq<int>([0, 1, 2, 3, 4]);
        var p   = seq >>> new C(2);
        Assert.That(p.Count, Is.EqualTo(5 * 4 / 2));
    }

    [Test]
    public void Test2()
    {
        new QuestionBuilder(["a"], "x", ["a", "b", "c", "d"]).ToQuestion();
        Assert.Pass();
    }

    [Test]
    public void Test3()
    {
        {
            var f = (new Frac(3, 2) + new Frac(1, 3));

            Assert.Multiple(() =>
            {
                Assert.That(f, Is.EqualTo(new Frac(11, 6)));
                Assert.That(f.Real, Is.EqualTo(new Frac(5, 6, 1)));
            });
        }

        {
            var q = (new Frac(2, 5) * new Frac(2, 4));

            Assert.That(q, Is.EqualTo(new Frac(4, 20)));
            Assert.That(q.Real, Is.EqualTo(new Frac(1, 5)));
        }
    }
}