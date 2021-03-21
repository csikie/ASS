using ASS.BLL.Services;
using ASS.Common.Enums;
using ASS.WEB.Models;
using ASS.WEB.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ASS.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly LoginService loginService;

        public HomeController(ILogger<HomeController> logger, LoginService loginService)
        {
            this.logger = logger;
            this.loginService = loginService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.Session.SetString("userRoles", await loginService.CreateUserRolesJson(User));
                HttpContext.Session.SetString("name", await loginService.CreateFullUserName(User));
                if (User.IsInRole(Role.Admin.ToString()))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (User.IsInRole(Role.Teacher.ToString()))
                {
                    return RedirectToAction("Index", "Teacher");
                }
                else if (User.IsInRole(Role.Instructor.ToString()))
                {
                    return RedirectToAction("Index", "Instructor");
                }
                else if (User.IsInRole(Role.Student.ToString()))
                {
                    return RedirectToAction("Index", "Student");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginUser)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            bool loginResult = await loginService.SignIn(loginUser.Username, loginUser.Password);

            if (loginResult)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult Logout()
        {
            loginService.SignOut();
            return View("Index");
        }

        [HttpPost]
        public IActionResult CultureManagment(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                                    new CookieOptions() { Expires = DateTimeOffset.Now.AddYears(30) });
            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
