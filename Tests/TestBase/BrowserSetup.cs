using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Tests.TestBase
{
    public class BrowserSetup
    {
        protected IWebDriver driver;

        [OneTimeSetUp]
        public void BrowserOneTimeSetUp()
        {
            driver = new ChromeDriver();
        }

        [OneTimeTearDown]
        public void BrowserOneTimeTearDown()
        {
            driver.Quit();
        }
    }
}
