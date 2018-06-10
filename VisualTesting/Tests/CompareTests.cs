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
            driver.Navigate().GoToUrl("https://wiadomosci.onet.pl/tylko-w-onecie/przed-smiercia-w-kopalni-chcial-przynajmniej-zobaczyc-slonce/w1tnz50");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }

        [Test]
        public void GetDifference()
        {
            JavaScript.RemoveElement(driver, "css", "header.pageHeader");
            Assert.That(Compare.GetDifference(driver, "removedHeader.png") == 0, "Check output");
        }
    }
}
