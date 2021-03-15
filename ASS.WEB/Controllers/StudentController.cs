using ASS.BLL.Services;
using ASS.WEB.Models.DTOs;
using ASS.WEB.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : BaseController
    {
        private readonly ILogger<StudentController> logger;
        private readonly StudentService studentService;

        public StudentController(ILogger<StudentController> logger, StudentService studentService) : base(studentService)
        {
            this.logger = logger;
            this.studentService = studentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<string> GetCourses()
        {
            string result = JsonConvert.SerializeObject((await studentService.GetCourses(User)).Select(x => new CourseDTO(x.Id,x.Name, x.Instructors.Select(y => new InstructorDTO(y.UserId,y.User.RealName, y.User.UserName)).ToArray())));
            return result;
        }

        public IActionResult CourseRegistration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CourseRegistration(CourseRegistrationViewModel model)
        {
            studentService.CourseRegistration(model.CourseIds, User);
            return View();
        }
    }
}
