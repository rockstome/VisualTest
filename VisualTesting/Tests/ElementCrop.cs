using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace VisualTesting.Tests
{
    public class ElementCrop
    {
        IWebDriver driver = new ChromeDriver();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver.Navigate().GoToUrl(Consts.formsHtml);
            driver.Manage().Window.Maximize();
        }

    }
}
