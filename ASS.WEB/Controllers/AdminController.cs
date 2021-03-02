using ASS.BLL.Services;
using ASS.Common.Enums;
using ASS.WEB.Models.DTOs;
using ASS.WEB.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly ILogger<AdminController> logger;
        private readonly AdminService adminService;

        public AdminController(ILogger<AdminController> _logger, AdminService _adminService)
        {
            logger = _logger;
            adminService = _adminService;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetString("name", adminService.FullName(User.Identity.Name));
            return View();
        }

        public async Task<string> GetTeachers()
        {
            string result = JsonConvert.SerializeObject((await adminService.GetUsersInRole(Role.Teacher)).Select(x => new TeacherDTO(x.Id,x.RealName, x.UserName)));
            return result;
        }

        public IActionResult CreateSubject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateSubject(CreateSubjectViewModel subject)
        {
            adminService.AddUserToSubject(subject.TeachersNeptunCode, subject.SubjectName);
            return Redirect("Index");
        }

        public string GetSubjects()
        {
            IEnumerable<SubjectDTO> subjects = adminService.GetSubjects()
                                                           .Select(x => new SubjectDTO(x.Id,x.Name, x.UserSubject.Select(y => new TeacherDTO(y.Id,y.User.RealName,y.User.UserName))
                                                                                                                 .ToArray()));
            string result = JsonConvert.SerializeObject(subjects);
            return result;
        }
            
        public string DeleteSubject(string models)
        {
            SubjectDTO subject = JsonConvert.DeserializeObject<List<SubjectDTO>>(models).FirstOrDefault();
            adminService.DeleteSubject(subject.Id);
            return GetSubjects();
            
        }

        public string UpdateSubject(string models)
        {
            SubjectDTO subject = JsonConvert.DeserializeObject<List<SubjectDTO>>(models).FirstOrDefault();
            adminService.UpdateSubject(subject.Id, subject.SubjectName, subject.TeachersName.Select(x => x.TeacherNeptun).ToArray());
            
            return GetSubjects();
        }
    }
}
