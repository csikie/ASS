using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.IO;
using System.Reflection;

namespace UITest
{
    [TestClass]
    public class EdgeDriverTest
    {
        // In order to run the below test(s), 
        // please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
        // to install Microsoft WebDriver.

        private EdgeDriver _driver;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            // Initialize edge driver 
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            _driver = new EdgeDriver(@"C:\Users\Csiki Erik\Desktop\ASS\UITest\bin\Debug\netcoreapp2.1", options);
        }

        [TestMethod]
        public void VerifyPageTitle()
        {
            // Replace with your own test logic
            _driver.Url = "https://localhost:44303/";
            _driver.FindElementById("Username").SendKeys("admin");
            _driver.FindElementById("Password").SendKeys("Ab1234");
            _driver.FindElement(By.XPath("//*[@id=\"Username\"]"));
            _driver.FindElementByName("login-btn").Click();
            Assert.AreEqual("Assignment Supervisor System - Index", _driver.Title);
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }
    }
}
