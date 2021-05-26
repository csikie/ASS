using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;

namespace UITest
{
    [TestClass]
    public class ASS_AdminTests
    {
        private EdgeDriver _driver;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Eager
            };
            _driver = new EdgeDriver(ConfigurationManager.AppSettings["DriverPath"], options);
            _driver.Url = ConfigurationManager.AppSettings["Url"];

            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[1]/input").SendKeys(ConfigurationManager.AppSettings["AdminUsername"]);
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[2]/input").SendKeys(ConfigurationManager.AppSettings["AdminPassword"]);
            _driver.FindElementByXPath("/html/body/main/div/div/div/form[1]/div[3]/button").Click();
            Thread.Sleep(1000);
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void Admin_CreateSubject_EmptyFields()
        {
            _driver.FindElementByXPath("/html/body/header/nav/div/div/ul/li[2]/a").Click();
            Thread.Sleep(1000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[3]/button").Click();
            Thread.Sleep(1000);

            string subjectNameErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/span").Text;
            string subjectTeacherErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/span").Text;
            Assert.IsTrue(subjectNameErrorText.Length > 0);
            Assert.IsTrue(subjectTeacherErrorText.Length > 0);
        }

        [TestMethod]
        public void Admin_CreateSubject_AlreadyUsedSubjectName()
        {
            _driver.FindElementByXPath("/html/body/header/nav/div/div/ul/li[2]/a").Click();
            Thread.Sleep(1000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/input").SendKeys("Funkcionális Programozás EA+GY");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/div/div").Click();
            Thread.Sleep(1000);
            _driver.FindElementByXPath("/html/body/div/div/div[2]/ul/li[1]/span").Click();

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[3]/button").Click();
            Thread.Sleep(1000);

            string error = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/div").Text;
            Assert.IsTrue(error.Length > 0);
        }
        
        [TestMethod]
        public void Admin_CreateSubject_Ok()
        {
            _driver.FindElementByXPath("/html/body/header/nav/div/div/ul/li[2]/a").Click();
            Thread.Sleep(1000);

            string subjectName = "Teszt subject";
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/input").SendKeys(subjectName);
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/div/div").Click();
            Thread.Sleep(1000);
            _driver.FindElementByXPath("/html/body/div/div/div[2]/ul/li[1]/span").Click();

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[3]/button").Click();
            Thread.Sleep(1000);

            Assert.AreEqual("ASS - Kezdõoldal", _driver.Title);
        }
        
        [TestMethod]
        public void Admin_CreateUser_Ok()
        {
            _driver.FindElementByXPath("/html/body/header/nav/div/div/ul/li[3]/a").Click();
            Thread.Sleep(1000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/input").SendKeys("qwertz");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/input").SendKeys("Teszt Pityu");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[3]/input").SendKeys("qwertz@inf.elte.hu");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[4]/input").SendKeys("Ab1234");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[5]/input").SendKeys("Ab1234");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[6]/div/div").Click();
            Thread.Sleep(1000);
            _driver.FindElementByXPath("/html/body/div/div/div[2]/ul/li[3]").Click();
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[7]/button").Click();
            Thread.Sleep(1000);
            Assert.AreEqual("ASS - Kezdõoldal", _driver.Title);
        }
        
        [TestMethod]
        public void Admin_CreateUser_EmptyFields()
        {
            _driver.FindElementByXPath("/html/body/header/nav/div/div/ul/li[3]/a").Click();
            Thread.Sleep(1000);
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[7]/button").Click();
            Thread.Sleep(1000);

            string neptunErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/span").Text;
            string nameErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/span").Text;
            string emailErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[3]/span").Text;
            string pwErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[4]/span").Text;
            string confpwErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[5]/span").Text;
            string roleErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[6]/span").Text;

            Assert.IsTrue(neptunErrorText.Length > 0);
            Assert.IsTrue(nameErrorText.Length > 0);
            Assert.IsTrue(emailErrorText.Length > 0);
            Assert.IsTrue(pwErrorText.Length > 0);
            Assert.IsTrue(confpwErrorText.Length > 0);
            Assert.IsTrue(roleErrorText.Length > 0);
        }

        [TestMethod]
        public void Admin_CreateUser_PasswordsNotMatch()
        {
            _driver.FindElementByXPath("/html/body/header/nav/div/div/ul/li[3]/a").Click();
            Thread.Sleep(1000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/input").SendKeys("qwertz");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/input").SendKeys("Teszt Pityu");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[3]/input").SendKeys("qwertz@inf.elte.hu");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[4]/input").SendKeys("Ab1234");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[5]/input").SendKeys("1234Ab");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[6]/div/div").Click();
            Thread.Sleep(1000);
            _driver.FindElementByXPath("/html/body/div/div/div[2]/ul/li[3]").Click();
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[7]/button").Click();
            Thread.Sleep(1000);

            string pwErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[4]/span").Text;
            string confpwErrorText = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[5]/span").Text;

            Assert.IsTrue(pwErrorText.Length > 0);
            Assert.IsTrue(confpwErrorText.Length > 0);
        }

        [TestMethod]
        public void Admin_CreateUser_ErrorWhileCreateUser()
        {
            _driver.FindElementByXPath("/html/body/header/nav/div/div/ul/li[3]/a").Click();
            Thread.Sleep(1000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[1]/input").SendKeys(ConfigurationManager.AppSettings["AdminUsername"]);
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/input").SendKeys("Teszt Pityu");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[3]/input").SendKeys("qwertz@inf.elte.hu");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[4]/input").SendKeys("Ab1234");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[5]/input").SendKeys("Ab1234");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[6]/div/div").Click();
            Thread.Sleep(1000);
            _driver.FindElementByXPath("/html/body/div/div/div[2]/ul/li[3]").Click();
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[7]/button").Click();
            Thread.Sleep(1000);

            string error = _driver.FindElementByXPath("/html/body/main/div/div/div[2]/div").Text;
            Assert.IsTrue(error.Length > 0);
        }

        [TestMethod]
        public void Admin_UpdateSubject_Ok()
        {
            string subjectName = _driver.FindElementByXPath("/html/body/main/div/div[1]/div[2]/div/div[3]/table/tbody/tr[2]/td[1]").Text;

            _driver.FindElementByXPath("/html/body/main/div/div[1]/div[2]/div/div[3]/table/tbody/tr[2]/td[3]/a[1]").Click();
            Thread.Sleep(1000);

            _driver.FindElementByXPath("/html/body/main/div/div[1]/div[2]/div/div[3]/table/tbody/tr[2]/td[1]/input").Clear();
            _driver.FindElementByXPath("/html/body/main/div/div[1]/div[2]/div/div[3]/table/tbody/tr[2]/td[1]/input").SendKeys("M");
            _driver.FindElementByXPath("/html/body/main/div/div[1]/div[2]/div/div[3]/table/tbody/tr[2]/td[3]/a[1]").Click();
            Thread.Sleep(1000);

            Assert.AreNotEqual(subjectName, _driver.FindElementByXPath("/html/body/main/div/div[1]/div[2]/div/div[3]/table/tbody/tr[2]/td[1]").Text);
        }

        [TestMethod]
        public void Admin_UpdateSubject_Error()
        {
            string subjectName = _driver.FindElementByXPath("/html/body/main/div/div[1]/div[2]/div/div[3]/table/tbody/tr[2]/td[1]").Text;

            _driver.FindElementByXPath("/html/body/main/div/div[1]/div[2]/div/div[3]/table/tbody/tr[2]/td[3]/a[1]").Click();
            Thread.Sleep(1000);
            _driver.FindElementByXPath("/html/body/main/div/div[1]/div[2]/div/div[3]/table/tbody/tr[2]/td[1]/input").Clear();
            _driver.FindElementByXPath("/html/body/main/div/div[1]/div[2]/div/div[3]/table/tbody/tr[2]/td[1]/input").SendKeys("Funkcionális programozás EA+GY");
            _driver.FindElementByXPath("/html/body/main/div/div[1]/div[2]/div/div[3]/table/tbody/tr[2]/td[3]/a[1]").Click();
            Thread.Sleep(2000);

            Assert.IsTrue(_driver.FindElementByXPath("/html/body/main/div/div/div/h1").Text.Length > 0);
            Assert.IsTrue(_driver.FindElementByXPath("/html/body/main/div/div/div/h2").Text.Length > 0);

            _driver.FindElementByXPath("/html/body/header/nav/div/a").Click();
            Thread.Sleep(1000);

            Assert.AreEqual(subjectName, _driver.FindElementByXPath("/html/body/main/div/div[1]/div[2]/div/div[3]/table/tbody/tr[2]/td[1]").Text);
        }

        [TestMethod]
        public void Admin_UpdateUser_Ok()
        {
            Thread.Sleep(1000);
            string name = _driver.FindElementByXPath("/html/body/main/div/div[2]/div[2]/div/div[3]/table/tbody/tr[1]/td[1]").Text;
            _driver.ExecuteScript("scroll(0,900)");
            Thread.Sleep(1000);
            _driver.FindElementByXPath("/html/body/main/div/div[2]/div[2]/div/div[3]/table/tbody/tr[1]/td[5]/a").Click();
            Thread.Sleep(1000);

            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[2]/input").SendKeys("M");
            _driver.FindElementByXPath("/html/body/main/div/div/div[2]/form/div[5]/button").Click();
            Thread.Sleep(1000);
            _driver.ExecuteScript("scroll(0,900)");
            Thread.Sleep(1000);
            Assert.AreNotEqual(name, _driver.FindElementByXPath("/html/body/main/div/div[2]/div[2]/div/div[3]/table/tbody/tr[1]/td[1]").Text);
        }
    }
}
