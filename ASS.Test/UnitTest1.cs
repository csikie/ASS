using ASS.BLL.Services;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ASS.Test
{
    public class Tests
    {
        LoginService LoginService { get; set; }
        [SetUp]
        public void Setup()
        {
            LoginService = new LoginService(new Mock<UserManager<User>>().Object, new Mock<SignInManager<User>>().Object);
        }

        [Test]
        public async Task Test1()
        {
            var res = await LoginService.SignIn("admin", "Ab1234");
            Assert.IsTrue(res);
        }
    }
}