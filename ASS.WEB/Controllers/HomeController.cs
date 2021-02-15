using ASS.Common.Enums;
using ASS.DAL.Models;
using ASS.WEB.Models;
using ASS.WEB.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ASS.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public HomeController(ILogger<HomeController> _logger, UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            logger = _logger;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(Role.Admin.ToString()))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return View("Privacy");
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

            SignInResult result = await signInManager.PasswordSignInAsync(loginUser.Username, loginUser.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            { 
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View("Index");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
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
