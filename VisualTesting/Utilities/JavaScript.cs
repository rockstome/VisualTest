using OpenQA.Selenium;
using System;

namespace VisualTesting.Utilities
{
    public static class JavaScript
    {
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
    }
}
