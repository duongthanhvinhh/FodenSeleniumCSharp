using System.Diagnostics;

namespace FodenApp;

public class UnitTest2
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test2()
    {
        Assert.Pass();
        Console.WriteLine("Test2");
        Debug.WriteLine("Test2");
    }
}
