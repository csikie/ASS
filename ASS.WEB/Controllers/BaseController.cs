using Microsoft.AspNetCore.Mvc;

namespace ASS.WEB.Controllers
{
    public abstract class BaseController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
