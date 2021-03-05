using ASS.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController : BaseController
    {
        private readonly ILogger<InstructorController> logger;
        private readonly InstructorService instructorService;

        public InstructorController(ILogger<InstructorController> logger, InstructorService instructorService) : base(instructorService)
        {
            this.logger = logger;
            this.instructorService = instructorService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
