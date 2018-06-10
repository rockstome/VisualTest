using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace VisualTesting.Utilities
{
    public static class SeleniumDriver
    {
        /// <summary>
        ///     Save screenshot of currently loaded page
        /// </summary>
        /// <param name="driver">WebDriver</param>
        public static void SaveScreenShotFromCurrentPage(IWebDriver driver, string fileName)
        {
            // TODO hardcoded path
            var testDataDirectory = @"C:\Users\tomas\Desktop\MyRepos\TestData\";
            var ss = ((ITakesScreenshot)driver).GetScreenshot();
            if (!Directory.Exists(testDataDirectory))
            {
                throw new IOException("Please check screenshots folder exists within test solution to save screenshots");
            }
            string path = $@"{testDataDirectory}{fileName}.png";
            ss.SaveAsFile(path, ScreenshotImageFormat.Png);
        }

        /// <summary>
        ///     Save screenshot of element on currently loaded page
        /// </summary>
        /// <param name="driver">WebDriver</param>
        /// <param name="by">Selector of element to take snapshot og</param>
        public static void SaveElementScreenShotFromCurrentPage(IWebDriver driver, By by, string fileName)
        {
            // TODO hardcoded path
            var testDataDirectory = @"C:\Users\tomas\Desktop\MyRepos\TestData\";

            var byteArray = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            var screenshot = new Bitmap(new MemoryStream(byteArray));
            try
            {
                IWebElement element = driver.FindElement(by);
                var croppedImage = new Rectangle(element.Location.X, element.Location.Y, element.Size.Width,
                    element.Size.Height);
                screenshot = screenshot.Clone(croppedImage, screenshot.PixelFormat);

                if (!Directory.Exists(testDataDirectory))
                {
                    throw new IOException(
                        "Please check screenshots folder exists within test solution to save screenshots");
                }
                string path = $@"{testDataDirectory}\{fileName}.png";
                screenshot.Save(path, ImageFormat.Png);
            }
            catch (Exception e)
            {
                string message = e.Message;
                throw new IOException("Could not find element to take a screenshot");
            }
        }

        // TODO robi tylko zdjecie elementu jest ten znajduje sie w widoku
        public static Bitmap TakeElementScreenShotFromCurrentPage(IWebDriver driver, By by)
        {
            var byteArray = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            var screenshot = new Bitmap(new MemoryStream(byteArray));
            IWebElement element = driver.FindElement(by);
            var croppedImage = new Rectangle(element.Location.X, element.Location.Y, element.Size.Width,
                element.Size.Height);
            return screenshot.Clone(croppedImage, screenshot.PixelFormat);
        }

        public static Bitmap TakeScreenShotFromCurrentPage(IWebDriver driver)
        {
            var byteArray = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            return new Bitmap(new MemoryStream(byteArray));
        }

        /// <summary>
        ///     Returns a screenshot of the full page image
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        /// <returns>Image of full page</returns>
        public static Image GetScreenShotOfFullPage(IWebDriver driver)
        {
            // Get the total size of the page
            var totalWidth = (int)(long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.offsetWidth"); //documentElement.scrollWidth");
            var totalHeight = (int)(long)((IJavaScriptExecutor)driver).ExecuteScript("return  document.body.parentNode.scrollHeight");
            // Get the size of the viewport
            var viewportWidth = (int)(long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.offsetWidth"); // TODO to jest zmienione, nie bedzie screenowac na boki
            var viewportHeight = (int)(long)((IJavaScriptExecutor)driver).ExecuteScript("return window.innerHeight");

            // We only care about taking multiple images together if it doesn't already fit
            if (totalWidth <= viewportWidth && totalHeight <= viewportHeight)
            {
                var screenshot = driver.TakeScreenshot();
                return ScreenshotToImage(screenshot);
            }
            // Split the screen in multiple Rectangles
            var rectangles = new List<Rectangle>();
            // Loop until the totalHeight is reached
            for (var y = 0; y < totalHeight; y += viewportHeight)
            {
                var newHeight = viewportHeight;
                // Fix if the height of the element is too big
                if (y + viewportHeight > totalHeight)
                {
                    newHeight = totalHeight - y;
                }
                // Loop until the totalWidth is reached
                for (var x = 0; x < totalWidth; x += viewportWidth)
                {
                    var newWidth = viewportWidth;
                    // Fix if the Width of the Element is too big
                    if (x + viewportWidth > totalWidth)
                    {
                        newWidth = totalWidth - x;
                    }
                    // Create and add the Rectangle
                    var currRect = new Rectangle(x, y, newWidth, newHeight);
                    rectangles.Add(currRect);
                }
            }
            // Build the Image
            var stitchedImage = new Bitmap(totalWidth, totalHeight);
            // Get all Screenshots and stitch them together
            var previous = Rectangle.Empty;
            // Start from upper left corner
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0)");
            foreach (var rectangle in rectangles)
            {
                // Wait for JavaScript and jQuery fully load if exist
                Waits.ForPageLoad(driver);
                // Calculate the scrolling (if needed)
                if (previous != Rectangle.Empty)
                {
                    var xDiff = rectangle.Right - previous.Right;
                    var yDiff = rectangle.Bottom - previous.Bottom;
                    // Scroll
                    ((IJavaScriptExecutor)driver).ExecuteScript(String.Format("window.scrollBy({0}, {1})", xDiff, yDiff));
                }
                // Take Screenshot
                var screenshot = driver.TakeScreenshot();
                // Build an Image out of the Screenshot
                var screenshotImage = ScreenshotToImage(screenshot);
                // Calculate the source Rectangle
                var sourceRectangle = new Rectangle(viewportWidth - rectangle.Width, viewportHeight - rectangle.Height, rectangle.Width, rectangle.Height);
                // Copy the Image
                using (var graphics = Graphics.FromImage(stitchedImage))
                {
                    graphics.DrawImage(screenshotImage, rectangle, sourceRectangle, GraphicsUnit.Pixel);
                }
                // Set the Previous Rectangle
                previous = rectangle;
            }
            return stitchedImage;
        }

        public static Image GetScreenShotOfFullPage(IWebDriver driver, By by)
        {
            var screenshot = ((Bitmap)GetScreenShotOfFullPage(driver));
            var element = driver.FindElement(by);

            var croppedImage = new Rectangle(
                    element.Location.X,
                    element.Location.Y,
                    Math.Min(element.Size.Width, screenshot.Size.Width - element.Location.X),
                    Math.Min(element.Size.Height, screenshot.Size.Height - element.Location.Y));

            return screenshot.Clone(croppedImage, screenshot.PixelFormat);
        }

        /// <summary>
        ///     Get screenshot of currently loaded page
        /// </summary>
        /// <param name="driver">WebDriver</param>
        /// <returns></returns>
        public static byte[] GetScreenshotOfCurrentPage(IWebDriver driver)
        {
            var bytes = ImageToByte(GetScreenShotOfFullPage(driver));
            return bytes;
        }

        /// <summary>
        ///     Get screenshot of element for currently loaded page
        /// </summary>
        /// <param name="driver">WebDriver</param>
        /// <param name="elementSelector">Selector to find element</param>
        /// <returns></returns>
        public static byte[] GetScreenshotOfCurrentPage(IWebDriver driver, By by)
        {
            var bytes = ImageToByte(GetScreenShotOfFullPage(driver, by));
            return bytes;
        }

        /// <summary>
        ///     Return screenshot as image
        /// </summary>
        /// <param name="screenshot">Screenshot to return as Image</param>
        private static Image ScreenshotToImage(Screenshot screenshot)
        {
            Image screenshotImage;
            using (var memStream = new MemoryStream(screenshot.AsByteArray))
            {
                screenshotImage = Image.FromStream(memStream);
            }
            return screenshotImage;
        }

        /// <summary>
        ///     Returns image as byte array
        /// </summary>
        /// <param name="img">Image to return as Byte array</param>
        private static byte[] ImageToByte(Image img)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        /// <summary>
        ///     Cover the specified dynamic element on the renedered page
        /// </summary>
        /// <param name="driver">WebDriver</param>
        /// <param name="elementSelector">Element Selector</param>
        public static void CoverDynamicElementBySelector(IWebDriver driver, By by)
        {
            IWebElement element = driver.FindElement(by);

            // Get position of element which we will overlay with a coloured box
            var elementX = element.Location.X; //element from top
            var elementY = element.Location.Y; // element from left
            var elementWidth = element.Size.Width;
            var elementHeight = element.Size.Height;

            // Set styling to place over the top of the dynamic content
            var style =
                string.Format(
                    "'position:absolute;top:{1}px;left:{0}px;width:{2}px;height:{3}px;color:white;background-color:#ffee11;text-align: center;'",
                    elementX, elementY, elementWidth, elementHeight);

            // Set javascript to execute on browser which will cover the dynamic content
            var replaceDynamicContentScript = "var div = document.createElement('div');div.setAttribute('style'," +
                                              style + ");document.body.appendChild(div); return true;";
            driver.ExecuteJavaScript(replaceDynamicContentScript);
        }
    }
}
