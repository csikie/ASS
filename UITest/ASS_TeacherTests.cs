using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Configuration;
using System.Threading;

namespace UITest
{
    [TestClass]
    public class ASS_TeacherTests
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

            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[1]/input").SendKeys(ConfigurationManager.AppSettings["TeacherUsername"]);
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[2]/input").SendKeys(ConfigurationManager.AppSettings["TeacherPassword"]);
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[3]/button").Click();
            Thread.Sleep(1000);
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void Teacher_EditCourse_Emptyfields()
        {
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/div/div[3]/table/tbody/tr[1]/td[1]/a").Click();
            Thread.Sleep(1000);
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/div/div[3]/table/tbody/tr[2]/td[2]/div/div[2]/table/tbody/tr[1]/td[3]/a").Click();
            Thread.Sleep(1000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/input").Clear();
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/div[1]/div/ul/li/span[2]").Click();

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/div[2]/button").Click();
            Thread.Sleep(1000);

            string errorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/span").Text;
            Assert.IsTrue(errorText.Length > 0);
        }

        [TestMethod]
        public void Teacher_EditCourse_Ok()
        {
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/div/div[3]/table/tbody/tr[1]/td[1]/a").Click();
            Thread.Sleep(1000);
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/div/div[3]/table/tbody/tr[2]/td[2]/div/div[2]/table/tbody/tr[1]/td[3]/a").Click();
            Thread.Sleep(1000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/input").SendKeys("EDITED");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/div[2]/button").Click();
            Thread.Sleep(1000);

            Assert.AreEqual("ASS - Kezdőoldal", _driver.Title);
        }

        [TestMethod]
        public void Teacher_CreateCourse_EmptyFields()
        {
            _driver.FindElementByXPath("/html/body/header/nav/div/div/ul/li[2]/a").Click();
            _driver.FindElementByXPath("/html/body/main/div/div/div/form/div[6]/button").Click();
            Thread.Sleep(1000);

            string errorText1 = _driver.FindElementByXPath("/html/body/main/div/div/div/form/div[1]/span").Text;
            string errorText2 = _driver.FindElementByXPath("/html/body/main/div/div/div/form/div[4]/span").Text;
            string errorText3 = _driver.FindElementByXPath("/html/body/main/div/div/div/form/div[5]/span").Text;

            Assert.IsTrue(errorText1.Length > 0);
            Assert.IsTrue(errorText2.Length > 0);
            Assert.IsTrue(errorText3.Length > 0);
        }

        [TestMethod]
        public void Teacher_CreateCourse_Ok()
        {
            _driver.FindElementByXPath("/html/body/header/nav/div/div/ul/li[2]/a").Click();
            Thread.Sleep(1000);


            _driver.FindElementByXPath("/html/body/main/div/div/div/form/div[1]/input").SendKeys("CreateCourse");

            _driver.FindElementByXPath("/html/body/main/div/div/div/form/div[3]/span/span/span[1]").Click();
            Thread.Sleep(1000);
            _driver.FindElementByXPath("/html/body/div[2]/div/div[3]/ul/li[1]").Click();

            _driver.FindElementByXPath("/html/body/main/div/div/div/form/div[5]/div/div").Click();
            Thread.Sleep(1000);
            _driver.FindElementByXPath("/html/body/div[1]/div/div[2]/ul/li/span").Click();

            _driver.FindElementByXPath("/html/body/main/div/div/div/form/div[6]/button").Click();

            Assert.AreEqual("ASS - Kezdőoldal", _driver.Title);
        }
    }
}
