using ASS.BLL.Services;
using ASS.Common.Enums;
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
            ViewBag.Username = adminService.GetFullName(User.Identity.Name);
            return View();
        }

        public async Task<string> GetTeachers()
        {
            string result = JsonConvert.SerializeObject((await adminService.GetUsersInRole(Role.Teacher)).Select(x => new TeacherDTO(x.Id,x.RealName, x.UserName)));
            return result;
        }

        [HttpPost]
        public IActionResult CreateSubject(CreateSubjectViewModel subject)
        {
            adminService.AddUserToSubject(subject.TeachersNeptunCode, subject.SubjectName);
            return Redirect("Index");
        }

        public string GetSubjects()
        {
            IEnumerable<SubjectDTO> subjects = adminService.GetSubjects().Select(x => new SubjectDTO(x.Id,x.Name, x.UserSubject.Select(y => new TeacherDTO(y.Id,y.User.RealName,y.User.UserName)).ToArray()));
            string result = JsonConvert.SerializeObject(subjects);
            return result;
        }
            
        public IActionResult DeleteSubject(string models)
        {
            List<SubjectDTO> subjects = JsonConvert.DeserializeObject<List<SubjectDTO>>(models);

            return View("Index");
        }

        public string UpdateSubject(string models)
        {
            SubjectDTO subject = JsonConvert.DeserializeObject<List<SubjectDTO>>(models).FirstOrDefault();
            adminService.UpdateSubject(subject.Id, subject.SubjectName, subject.TeachersName.Select(x => x.TeacherNeptun).ToArray());
            
            return GetSubjects();
            //return View("Index");
        }
    }
}
