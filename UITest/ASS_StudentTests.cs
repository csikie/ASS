using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Configuration;

namespace UITest
{
    [TestClass]
    public class ASS_StudentTests
    {
        private EdgeDriver _driver;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            _driver = new EdgeDriver(ConfigurationManager.AppSettings["DriverPath"], options);
            _driver.Url = ConfigurationManager.AppSettings["Url"];

            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[1]/input").SendKeys(ConfigurationManager.AppSettings["StudentUsername"]);
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[2]/input").SendKeys(ConfigurationManager.AppSettings["StudentPassword"]);
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[3]/button").Click();
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }
    }
}
