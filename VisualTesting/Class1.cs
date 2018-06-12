using NUnit.Framework;
using Tests.TestBase;
using VisualTesting.Utilities;

namespace Tests
{
    public class Class1 : AppSetup
    {
        public Class1() : base(Helper.GetUser("60001617")) { }

        [Test]
        public void Test1()
        {
            Compare.GetDifference(driver, "firstScreen");
            System.Console.WriteLine(user["password"]);
        }

        [Test]
        public void Test2()
        {
            System.Console.WriteLine(user["millecode"]);
        }
    }
}
