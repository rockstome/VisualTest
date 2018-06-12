using NUnit.Framework;
using System.Collections.Generic;

namespace Tests.TestBase
{
    public class AppSetup : BrowserSetup
    {
        protected Dictionary<string,string> user;

        public AppSetup(Dictionary<string, string> user)
        {
            this.user = user;
        }

        [OneTimeSetUp]
        public void AppOneTimeSetUp()
        {
            driver.Navigate().GoToUrl($"http://www.{user["url"]}.com");
        }

        [SetUp]
        public void AppSetUp()
        {
            // navigate to home page
        }

        [OneTimeTearDown]
        public void AppOneTimeTearDown()
        {
            // do logout
        }
    }
}
