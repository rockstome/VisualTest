using OpenQA.Selenium;
using System;

namespace VisualTesting.Utilities
{
    public static class JavaScript
    {
        /// <summary>
        ///     Remove the specified element from the DOM.
        /// </summary>
        /// <param name="driver">Webdriver.</param>
        /// <param name="method">Method to find element, supported methods: id, name, class, css, xpath.</param>
        /// <param name="selector"></param>
        public static void RemoveElement(IWebDriver driver, string method, string selector)
        {
            string script = null;
            if (string.Equals(method, "id", StringComparison.OrdinalIgnoreCase))
            {
                script = $"var element = document.getElementById('{selector}');" +
                    $"return element.parentNode.removeChild(element);";
                ((IJavaScriptExecutor)driver).ExecuteScript(script);
            }
            else if(string.Equals(method, "name", StringComparison.OrdinalIgnoreCase))
            {
                script = $"var element = document.getElementsByName('{selector}')[0];" +
                    $"return element.parentNode.removeChild(element);";
                ((IJavaScriptExecutor)driver).ExecuteScript(script);
            }
            else if (string.Equals(method, "class", StringComparison.OrdinalIgnoreCase))
            {
                script = $"var element = document.getElementsByClassName('{selector}')[0];" +
                    $"return element.parentNode.removeChild(element);";
                ((IJavaScriptExecutor)driver).ExecuteScript(script);
            }
            else if (string.Equals(method, "css", StringComparison.OrdinalIgnoreCase))
            {
                script = $"var element = document.querySelector('{selector}');" +
                    $"return element.parentNode.removeChild(element);";
                ((IJavaScriptExecutor)driver).ExecuteScript(script);
            }
            else if (string.Equals(method, "xpath", StringComparison.OrdinalIgnoreCase))
            {
                script = $@"var element = document.evaluate(""{selector}"", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                    $"return element.parentNode.removeChild(element);";
                ((IJavaScriptExecutor)driver).ExecuteScript(script);
            }
            else
            { 
                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Cover the specified dynamic element on the renedered page.
        /// </summary>
        /// <param name="driver">WebDriver.</param>
        /// <param name="by">Element selector.</param>
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
            ((IJavaScriptExecutor)driver).ExecuteScript(replaceDynamicContentScript);
        }
    }
}
