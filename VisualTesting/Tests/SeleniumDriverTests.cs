using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using VisualTesting.Utilities;

namespace VisualTesting.Tests
{
    public class SeleniumDriverTests
    {
        public IWebDriver driver = new ChromeDriver();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver.Navigate().GoToUrl(@"C:\Users\tomas\Desktop\MyRepos\forms.html");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }

        [Test]
        public void SaveScreenShotFromCurrentPage()
        {
            string testDataDirectory = @"C:\Users\tomas\Desktop\MyRepos\TestData\";
            string fileName = new Random().Next(10000, 100000).ToString();
            string path = testDataDirectory + fileName + ".png";

            DirectoryInfo di = new DirectoryInfo(testDataDirectory);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            Assert.That(File.Exists(path), Is.EqualTo(false));
            SeleniumDriver.SaveScreenShotFromCurrentPage(driver, fileName);
            Assert.That(File.Exists(path), Is.EqualTo(true));
        }

        [Test]
        public void SaveElementScreenShotFromCurrentPage()
        {
            string testDataDirectory = @"C:\Users\tomas\Desktop\MyRepos\TestData\";
            string fileName = new Random().Next(10000, 100000).ToString();
            string path = testDataDirectory + fileName + ".png";

            DirectoryInfo di = new DirectoryInfo(testDataDirectory);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            Assert.That(File.Exists(path), Is.EqualTo(false));
            SeleniumDriver.SaveElementScreenShotFromCurrentPage(driver, By.Id("all"), fileName);
            Assert.That(File.Exists(path), Is.EqualTo(true));
        }

        [Test]
        public void CoverDynamicElementBySelector()
        {
            string testDataDirectory = @"C:\Users\tomas\Desktop\MyRepos\TestData\";
            string fileName = new Random().Next(10000, 100000).ToString();
            string path = testDataDirectory + fileName + ".png";

            DirectoryInfo di = new DirectoryInfo(testDataDirectory);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            Assert.That(File.Exists(path), Is.EqualTo(false));
            SeleniumDriver.CoverDynamicElementBySelector(driver, By.Id("form2"));
            SeleniumDriver.SaveScreenShotFromCurrentPage(driver, fileName);
            Assert.That(File.Exists(path), Is.EqualTo(true));
        }
    }
}
