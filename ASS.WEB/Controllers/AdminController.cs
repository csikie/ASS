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

        [HttpPost]
        public string DeleteSubject(string models)
        {
            SubjectDTO subject = JsonConvert.DeserializeObject<List<SubjectDTO>>(models).FirstOrDefault();
            adminService.DeleteSubject(subject.Id);
            return GetSubjects();

        }

        [HttpPost]
        public IActionResult UpdateSubject(string models)
        {
            AdminSubjectDTO subject = JsonConvert.DeserializeObject<List<AdminSubjectDTO>>(models).FirstOrDefault();

            try
            {
                adminService.UpdateSubject(subject.Id, subject.SubjectName, subject.TeachersName.Select(x => x.TeacherNeptun).ToArray());
            }
            catch (ArgumentException ex) when (ex.Message.Contains("foglalt"))
            {
                return BadRequest(new { errors = new { code = "400", reason = "SubjectNameAlreadyUsed" } });
            }

            return Ok(GetSubjects());
        }

        public IActionResult Read_UserGrid()
        {
            try
            {
                List<UserDTO> users = adminService.GetAllUser().Select(x => new UserDTO(x.RealName, x.UserName, x.Email, adminService.GetUserRoles(x.Id)) { Id = x.Id}).ToList();
                return Ok(JsonConvert.SerializeObject(users));
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = new { code = "400", reason = ex.Message } });
            }

        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    adminService.CreateUser(model.RealName, model.UserName, model.Email, model.Password, model.Roles);
                    return RedirectToAction("Index");
                }
                catch (Exception ex) when (ex.Message.Contains("Nem sikerült létrehozni a felhasználót!"))
                {
                    ModelState.AddModelError("", "ErrorWhileCreatingUser");
                    return View();
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            return View();
        }

        public IActionResult GetAllRole()
        {
            try
            {
                List<object> roles = new List<object>();
                foreach (Role role in Enum.GetValues(typeof(Role)))
                {
                    roles.Add(new { Role = role.ToString() });
                }
                return Ok(JsonConvert.SerializeObject(roles.ToArray()));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult UpdateUser(int id)
        {
            var data = adminService.GetUserData(id);
            UpdateUserViewModel model = new UpdateUserViewModel(id, data.RealName, data.UserName, data.Email, adminService.GetUserRoles(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUser(UpdateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    adminService.UpdateUser(model.Id, model.UserName, model.RealName, model.Email, model.Roles);
                    return RedirectToAction("Index", "Admin");
                }
                catch (Exception ex) when (ex.Message.Contains("nem egyezik"))
                {
                    ModelState.AddModelError("", "UserNameCompareError");
                    return View(model);
                }
                catch (Exception ex) when (ex.Message.Contains("eltávolítása során"))
                {
                    ModelState.AddModelError("", "RoleRemoveError");
                    return View(model);
                }
                catch (Exception ex) when (ex.Message.Contains("hozzáadása során"))
                {
                    ModelState.AddModelError("", "RoleAddError");
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
