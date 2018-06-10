using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;
using System.Drawing.Imaging;
using VisualTesting.Utilities;

namespace VisualTesting.Tests
{
    public class ElementCrop
    {
        IWebDriver driver = new ChromeDriver();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver.Navigate().GoToUrl(@"C:\Users\tomas\Desktop\MyRepos\forms.html");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            var id = "form1";
            var by = By.Id(id);
            var element = driver.FindElement(by);
            var elementScreen = SeleniumDriver.TakeElementScreenShotFromCurrentPage(driver, by);
            var croppedImage = new Rectangle(element.Location.X, element.Location.Y, 
                                            element.Size.Width, element.Size.Height);
            var fullScreen = SeleniumDriver.TakeScreenShotFromCurrentPage(driver);
            var cuttedScreen = fullScreen.Clone(croppedImage, fullScreen.PixelFormat);

            Assert.That(cuttedScreen.Height, Is.EqualTo(elementScreen.Height));
            Assert.That(cuttedScreen.Width, Is.EqualTo(elementScreen.Width));

            cuttedScreen.Save(@"C:\Users\tomas\Desktop\MyRepos\TestData\cutted.bmp", ImageFormat.Bmp);
            elementScreen.Save(@"C:\Users\tomas\Desktop\MyRepos\TestData\element.bmp", ImageFormat.Bmp);
        }
    }
}
