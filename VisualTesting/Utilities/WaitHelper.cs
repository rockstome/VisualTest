using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace VisualTesting.Utilities
{
    public class Waits
    {
        public static bool ForPageLoad(IWebDriver driver, int seconds = 30)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));

            Func<IWebDriver, bool> JsLoad = (d) =>
            {
                return (string)((IJavaScriptExecutor)d).ExecuteScript("return document.readyState;") == "complete";
            };

            Func<IWebDriver, bool> jQueryLoad = (d) =>
            {
                try
                {
                    return (Int64)((IJavaScriptExecutor)d).ExecuteScript("return jQuery.active;") == 0;
                }
                catch
                {
                    return true;
                }
            };

            return wait.Until(JsLoad) && wait.Until(jQueryLoad);
        }
    }
}
