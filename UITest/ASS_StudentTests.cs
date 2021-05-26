using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Configuration;
using System.Threading;

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
            Thread.Sleep(1000);
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void Student_CourseRegistration_EmptyField()
        {
            _driver.FindElementByXPath("/html/body/header/nav/div/div/ul/li[2]/a").Click();
            Thread.Sleep(1000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/button").Click();
            Thread.Sleep(1000);

            string errorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/span").Text;
            Assert.IsTrue(errorText.Length > 0);
        }

        [TestMethod]
        public void Student_CourseRegistration_Ok()
        {
            _driver.FindElementByXPath("/html/body/header/nav/div/div/ul/li[2]/a").Click();
            Thread.Sleep(2000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/div/div").Click();
            Thread.Sleep(2000);

            _driver.FindElementByXPath("/html/body/div/div/div[2]/ul/li[1]").Click();
            Thread.Sleep(2000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/button").Click();
            Thread.Sleep(2000);

            Assert.AreEqual("ASS - Kezdőoldal", _driver.Title);
        }

        [TestMethod]
        public void Student_SubmitSolution_Ok()
        {
            _driver.Url = "http://localhost:5000/Student/Assignment/1";

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/textarea").SendKeys("Teszt megoldás.");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/button").Click();
            Thread.Sleep(1000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[1]/div/div[3]/div/div/div[1]/button").Click();
            Thread.Sleep(500);

            string sol = _driver.FindElementByXPath("/html/body/main/div/div/div[1]/div/div[3]/div/div/div[2]/div/code/pre").Text;
            Assert.IsTrue(sol.Length > 0);
        }
    }
}
