using ASS.BLL.Services;
using ASS.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> logger;
        private readonly IAdminService adminService;

        public AdminController(ILogger<AdminController> _logger, IAdminService _adminService)
        {
            logger = _logger;
            adminService = _adminService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
