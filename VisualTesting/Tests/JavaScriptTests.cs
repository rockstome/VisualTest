using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using VisualTesting.Utilities;

namespace VisualTesting.Tests
{
    public class JavaScriptTests
    {
        public IWebDriver driver = new ChromeDriver();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(@"C:\Users\tomas\Desktop\MyRepos\forms.html");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }

        [Test]
        public void RemoveElementById()
        {
            JavaScript.RemoveElement(driver, "id", "form1");
        }

        [Test]
        public void RemoveElementByName()
        {
            JavaScript.RemoveElement(driver, "name", "form2");
        }

        [Test]
        public void RemoveElementByXPath()
        {
            JavaScript.RemoveElement(driver, "xpath", "//*[@name='form2']//input[@type='submit']");
        }

        [Test]
        public void RemoveElementByClass()
        {
            JavaScript.RemoveElement(driver, "class", "form3");
        }

        [Test]
        public void RemoveElementByCss()
        {
            JavaScript.RemoveElement(driver, "css", "form#form1");
        }
    }
}
