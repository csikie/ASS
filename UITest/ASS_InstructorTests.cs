using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Configuration;
using System.Threading;

namespace UITest
{
    [TestClass]
    public class ASS_InstructorTests
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

            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[1]/input").SendKeys(ConfigurationManager.AppSettings["InstructorUsername"]);
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[2]/input").SendKeys(ConfigurationManager.AppSettings["InstructorPassword"]);
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[3]/button").Click();
            Thread.Sleep(1000);
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void Instructor_ApproveRegistration()
        {
            _driver.ExecuteScript("scroll(0,900)");
            Thread.Sleep(1000);
            _driver.FindElementByXPath("/html/body/main/div/div[2]/div[2]/div/div[3]/table/tbody/tr[2]/td[6]/a[2]").Click();

            _driver.Navigate().Refresh();
            Thread.Sleep(1000);

            Assert.IsTrue(_driver.FindElementByXPath("/html/body/main/div/div[1]/div[2]/div[1]/table/tbody/tr/th").Text.Length > 0);
        }

        [TestMethod]
        public void Instructor_CreateAssignment_Emptyfields()
        {
            _driver.FindElementByXPath("/html/body/header/nav/div/div/ul/li[2]/a").Click();
            Thread.Sleep(1000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/div[1]/span[1]/span/input").Clear();
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/div[2]/span[1]/span/input").Clear();
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[3]/div/textarea").Clear();
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[5]/div[1]/button").Click();
            Thread.Sleep(500);

            string nameErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/div/span").Text;
            string startErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/div[1]/span[2]").Text;
            string endErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/div[2]/span[2]").Text;
            string descErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[3]/div/span").Text;
            string groupErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[4]/div/span").Text;

            Assert.IsTrue(nameErrorText.Length > 0);
            Assert.IsTrue(startErrorText.Length > 0);
            Assert.IsTrue(endErrorText.Length > 0);
            Assert.IsTrue(descErrorText.Length > 0);
            Assert.IsTrue(groupErrorText.Length > 0);
        }

        [TestMethod]
        public void Instructor_CreateAssignment_WrongRange()
        {
            _driver.FindElementByXPath("/html/body/header/nav/div/div/ul/li[2]/a").Click();
            Thread.Sleep(1000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/div/input").SendKeys("Teszt");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/div[2]/span[1]/span/input").Clear();
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/div[2]/span[1]/span/input").SendKeys(DateTime.Now.AddDays(-4).ToString("yyyy.MM.dd HH:mm"));
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[4]/div/div/div").Click();
            Thread.Sleep(1000);
            _driver.FindElementByXPath("/html/body/div[3]/div/div[2]/ul/li[1]").Click();

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[5]/div[1]/button").Click();
            Thread.Sleep(1000);

            string temp = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/div").Text;
            Assert.IsTrue(temp.Length > 0);
        }

        [TestMethod]
        public void Instructor_CreateAssignment_Ok()
        {
            _driver.FindElementByXPath("/html/body/header/nav/div/div/ul/li[2]/a").Click();
            Thread.Sleep(1000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/div/input").SendKeys("Teszt");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/div[2]/span[1]/span/input").Clear();
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/div[2]/span[1]/span/input").SendKeys(DateTime.Now.ToString("yyyy.MM.dd HH:mm"));
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[4]/div/div/div").Click();
            Thread.Sleep(1000);
            _driver.FindElementByXPath("/html/body/div[3]/div/div[2]/ul/li[1]").Click();

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[5]/div[1]/button").Click();
            Thread.Sleep(1000);

            string temp = _driver.FindElementByXPath("/html/body/main/div/div[1]/div[2]/div[1]/table/thead/tr/th[2]").Text;
            Assert.IsTrue(temp.Length > 0);
        }

        [TestMethod]
        public void Instructor_EvaluateAssignment()
        {
            _driver.FindElementByXPath("/html/body/main/div/div[1]/div[2]/div[2]/table/tbody/tr/td/a").Click();
            Thread.Sleep(1000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[5]/div/div/form/div[1]/input").SendKeys("Teszt értékelés");
            _driver.FindElementByXPath("/html/body/main/div/div/div[5]/div/div/form/div[2]/button").Click();
            Thread.Sleep(1000);

            Assert.AreEqual("ASS - Kezdőoldal", _driver.Title);
        }
    }
}
