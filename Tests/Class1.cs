using NUnit.Framework;
using Tests.TestBase;

namespace Tests
{
    public class Class1 : AppSetup
    {
        public Class1() : base(Helper.GetUser("60001617")) { }

        [Test]
        public void Test1()
        {
            System.Console.WriteLine(user["password"]);
        }

        [Test]
        public void Test2()
        {
            System.Console.WriteLine(user["millecode"]);
        }
    }
}
