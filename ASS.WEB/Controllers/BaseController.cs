using ASS.BLL.Services;
using ASS.WEB.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ASS.WEB.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly BaseService service;
        protected BaseController(BaseService service)
        {
            this.service = service ?? throw new ArgumentNullException();
        }

        public async Task<IActionResult> Profile()
        {
            var user = await service.GetUserData(User);
            UserDTO res = new UserDTO(user.RealName, user.UserName, user.Email, await service.GetUserRoles(User));

            return View(res);
        }

        [HttpPost]
        public IActionResult CultureManagment(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                                    new CookieOptions() { Expires = DateTimeOffset.Now.AddYears(30) });
            return RedirectToAction(nameof(Profile));
        }

    }
}
