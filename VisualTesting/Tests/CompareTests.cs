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
            driver.Navigate().GoToUrl(@"C:\Users\tomas\Desktop\MyRepos\forms.html");
            //driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }

        [Test]
        public void GetDifference()
        {
            Assert.That(Compare.GetDifference(driver, "forms.png") == 0, "Check output");
        }
    }
}
