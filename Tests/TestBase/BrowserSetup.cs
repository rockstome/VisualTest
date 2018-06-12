using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Tests.TestBase
{
    public class BrowserSetup : BaseSetup
    {
        protected IWebDriver driver;

        [OneTimeSetUp]
        public void BrowserOneTimeSetUp()
        {
            driver = new ChromeDriver(); // TODO = new Browser.GetBrowser();
        }

        [OneTimeTearDown]
        public void BrowserOneTimeTearDown()
        {
            driver.Quit();
        }
    }
}
