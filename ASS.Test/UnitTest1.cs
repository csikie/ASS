using ASS.BLL.Services;
using ASS.DAL;
using ASS.DAL.Models;
using ASS.WEB;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASS.Test
{
    public class Tests
    {
        LoginService LoginService { get; set; }
        ASSContext Context { get; set; }
        
        [OneTimeSetUp]
        public void Setup()
        {
            Context = new ASSContext(new DbContextOptionsBuilder<ASSContext>().UseMySQL("server=localhost;database=ASS;uid=root;password=Kicsijoe18").Options);
        }
        [OneTimeTearDown]
        public void Cleanup()
        {
            Context.Dispose();
        }

        [Test]
        public void Test1()
        {
            var res = Context.Users.Find(1);
            Assert.IsTrue(res.UserName == "admin");
        }
    }
}