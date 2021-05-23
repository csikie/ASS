using Microsoft.AspNetCore.Mvc;
using ASS.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ASS.WEB.Models.ViewModels;
using Newtonsoft.Json;
using ASS.Common.Enums;
using ASS.WEB.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ASS.WEB.Controllers
{
    [Authorize(Roles = "Teacher")]
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
        
        public IActionResult CreateCourse()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCourse(CreateCourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    teacherService.CreateCourse(model.InstructorsNeptunCode, model.SubjectId.Value, model.CourseName);
                    return RedirectToAction("Index", "Teacher");
                }
                catch (ArgumentException ex) when (ex.Message.Contains("foglalt"))
                {
                    ModelState.AddModelError("","CourseNameAlreadyUsed");
                }
            }
            return View();
        }

        public async Task<string> Read_SubjectGrid()
        {
            var a = (await teacherService.GetSubjects(User)).Select(x => new TeacherSubjectDTO(x.Id, x.Name, x.Courses.Select(y => new CourseDTO(y.Id, y.Name, y.Instructors.Select(z => new InstructorDTO(z.UserId,z.User.RealName,z.User.UserName)).ToArray())).ToArray()));
            return JsonConvert.SerializeObject(a);
        }

        public async Task<string> GetSubjects()
        {
            string result = JsonConvert.SerializeObject((await teacherService.GetSubjects(User)).Select(x => new SubjectDTO(x.Id,x.Name)).ToList());
            return result;
        }
        
        public async Task<string> GetInstructors()
        {
            string result = JsonConvert.SerializeObject((await teacherService.GetUsersInRole(Role.Instructor)).Select(x => new InstructorDTO(x.Id, x.RealName, x.UserName)));
            return result;
        }

        public IActionResult EditCourse(int id)
        {
            var model = teacherService.GetCourse(id);
            return View(new EditCourseViewModel(model.Id, model.Name)
                        {
                            CurrentInstructorsJSON = JsonConvert.SerializeObject(model.Instructors.Select(x => new InstructorDTO(x.Id,x.User.RealName,x.User.UserName)).ToList())
                        });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCourse(EditCourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    teacherService.EditCourse(model.Id, model.CourseName, model.InstructorUserNames == null ? new string[0] : model.InstructorUserNames);
                    return RedirectToAction("Index","Teacher");
                }
                catch (Exception)
                {
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
