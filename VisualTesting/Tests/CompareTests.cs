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
            driver.Navigate().GoToUrl("https://stackoverflow.com/questions/1821167/selenium-screenshots-using-rspec");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }

        [Test]
        public void WithBars()
        {
            Assert.That(Compare.GetDifference(driver, "withBars.png"), Is.EqualTo(0));
        }

        [Test]
        public void WithoutBars()
        {
            driver.FindElement(By.CssSelector("div#js-gdpr-consent-banner button")).Click();
            JavaScript.RemoveElement(driver, "css", "header.top-bar");
            Assert.That(Compare.GetDifference(driver, "withBars.png"), Is.EqualTo(0));
        }
    }
}
