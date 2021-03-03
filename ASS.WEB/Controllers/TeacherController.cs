using Microsoft.AspNetCore.Mvc;
using ASS.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ASS.WEB.Controllers
{
    public class TeacherController : BaseController
    {
        private readonly ILogger<TeacherController> logger;
        private readonly TeacherService teacherService;

        public TeacherController(ILogger<TeacherController> logger, TeacherService teacherService) : base(teacherService)
        {
            this.logger = logger;
            this.teacherService = teacherService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
