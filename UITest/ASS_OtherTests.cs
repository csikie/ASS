using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace UITest
{
    [TestClass]
    public class ASS_OtherTests
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
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void ChangeLanguageTest()
        {
            Assert.AreEqual("ASS - Főoldal", _driver.Title);
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[2]/div/button[2]").Click();
            Assert.AreEqual("ASS - Home page", _driver.Title);
        }

        [TestMethod]
        public void EmptyLoginFieldsTest()
        {
            string userNameErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[1]/span").Text;
            string passwordErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[2]/span").Text;
            Assert.AreEqual("", userNameErrorText);
            Assert.AreEqual("", passwordErrorText);

            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[3]/button").Click();
            userNameErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[1]/span").Text;
            passwordErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[2]/span").Text;

            Assert.IsTrue(userNameErrorText.Length > 0);
            Assert.IsTrue(passwordErrorText.Length > 0);
        }

        [TestMethod]
        public void LengthUsernameFieldsTest()
        {
            string userNameErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[1]/span").Text;
            string passwordErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[2]/span").Text;
            Assert.AreEqual("", userNameErrorText);
            Assert.AreEqual("", passwordErrorText);

            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[2]/input").SendKeys("Ab1234");
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[1]/input").SendKeys("a");

            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[3]/button").Click();
            userNameErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[1]/span").Text;
            passwordErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[2]/span").Text;

            Assert.IsTrue(userNameErrorText.Length > 0);
            Assert.IsTrue(passwordErrorText.Length == 0);
        }

        [TestMethod]
        public void LoginWrongUserTest()
        {
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[2]/input").SendKeys("Ab1234");
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[1]/input").SendKeys("asdasd");

            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[3]/button").Click();

            string error = _driver.FindElementByXPath("/html/body/main/div/div/div/div[2]/span").Text;
            Assert.IsTrue(error.Length > 0);
        }

        [TestMethod]
        public void LoginTest()
        {
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[2]/input").SendKeys("Ab1234");
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[1]/input").SendKeys("admin");
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[3]/button").Click();

            Assert.IsTrue(!_driver.Url.Contains("Home"));
        }

        [TestMethod]
        public void LogoutTest()
        {
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[2]/input").SendKeys("Ab1234");
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[1]/input").SendKeys("admin");
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[3]/button").Click();

            _driver.FindElementByXPath("/html/body/header/nav/div/div/ul/li[4]/a").Click();

            Assert.AreEqual("Assignment Supervisor System", _driver.FindElementByXPath("/html/body/header/div/div/div/span").Text);
        }
    }
}
