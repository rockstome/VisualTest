using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using VisualTesting.Utilities;

namespace VisualTesting.Tests
{
    public class CompareTests
    {
        public IWebDriver driver = new ChromeDriver();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.onet.pl/");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }

        [Test]
        public void GetDifference()
        {
            Assert.That(Compare.GetDifference(driver, "forms.png", By.Id("cipa")) == 0, "Check output");
        }
    }
}
