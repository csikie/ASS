﻿using ASS.BLL.Services;
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
    public class AdminController : BaseController
    {
        private readonly ILogger<AdminController> logger;
        private readonly AdminService adminService;

        public AdminController(ILogger<AdminController> logger, AdminService adminService) : base(adminService)
        {
            this.logger = logger;
            this.adminService = adminService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<string> GetTeachers()
        {
            string result = JsonConvert.SerializeObject((await adminService.GetUsersInRole(Role.Teacher)).Select(x => new TeacherDTO(x.Id, x.RealName, x.UserName)));
            return result;
        }

        public IActionResult CreateSubject()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSubject(CreateSubjectViewModel subject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    adminService.AddUserToSubject(subject.TeachersNeptunCode, subject.SubjectName);
                    return View("Index");
                }
                catch (ArgumentException ex) when (ex.Message.Contains("foglalt"))
                {
                    ModelState.AddModelError("", "SubjectNameAlreadyUsed");
                    return View();
                }
            }
            return View();
            
        }

        public string GetSubjects()
        {
            IEnumerable<SubjectDTO> subjects = adminService.GetSubjects()
                                                           .Select(x => new AdminSubjectDTO(x.Id, x.Name, x.UserSubject.Select(y => new TeacherDTO(y.Id, y.User.RealName, y.User.UserName))
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
            AdminSubjectDTO subject = JsonConvert.DeserializeObject<List<AdminSubjectDTO>>(models).FirstOrDefault();

            try
            {
                adminService.UpdateSubject(subject.Id, subject.SubjectName, subject.TeachersName.Select(x => x.TeacherNeptun).ToArray());
            }
            catch (ArgumentException ex) when (ex.Message.Contains("foglalt"))
            {
                return $"\"errors\": [{{ \"code\": \"400\",\"reason\": \"{ex.Message}\"}}]";
            }

            return GetSubjects();
        }
    }
}
