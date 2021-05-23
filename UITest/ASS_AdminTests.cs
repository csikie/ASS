using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace UITest
{
    [TestClass]
    public class ASS_AdminTests
    {

        private EdgeDriver _driver;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            // Initialize edge driver 
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            _driver = new EdgeDriver(ConfigurationManager.AppSettings["DriverPath"], options);
            _driver.Url = ConfigurationManager.AppSettings["Url"];
        }

        [TestMethod]
        public void VerifyPageTitle()
        {
            // Replace with your own test logic
            _driver.FindElementById("Username").SendKeys("admin");
            _driver.FindElementById("Password").SendKeys("Ab1234");
            _driver.FindElement(By.XPath("//*[@id=\"Username\"]"));
            _driver.FindElementByName("login-btn").Click();
            Assert.AreEqual("ASS - Kezdõoldal", _driver.Title);
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }
    }
}
