using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BritInsurance_Automation
{
    public class ExplicitWaitHelperClass
    {
        protected IWebDriver _driver;
        public ExplicitWaitHelperClass(IWebDriver driver)
        {
            this._driver = driver;
        }

        public IWebElement WaitForElement(string locatorValue)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
                var element = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(locatorValue)));
                    return element;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IReadOnlyCollection<IWebElement> WaitForElementXpathList(string locatorValue)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
                IReadOnlyCollection<IWebElement> element = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(locatorValue)));
                return element;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
