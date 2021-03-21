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

        public IActionResult CreateAssignment()
        {
            return View(new CreateAssignmentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAssignment(CreateAssignmentViewModel assignment)
        {
            if (ModelState.IsValid)
            {
                instructorService.CreateAssignment(assignment.Name, assignment.Description, assignment.StartDate, assignment.EndDate, assignment.CourseIds);

            }
            return View();
        }

        public async Task<string> GetCourses()
        {
            string result = JsonConvert.SerializeObject((await instructorService.GetCourses(User)).Select(x => new CourseDTO(x.Id, x.Name, null)));
            return result;
        }

        public async Task<string> GetPendingList()
        {
            IEnumerable<PendingDTO> pendingList = (await instructorService.GetPendingList(User)).Select(x => new PendingDTO(x.Id, x.User.RealName, x.User.UserName, x.Course.Name, x.Course.Subject.Name));
            string result = JsonConvert.SerializeObject(pendingList);
            return result;
        }

        [HttpPost]
        public IActionResult ProcessPendingStatus(int id, bool isApprove)
        {
            try
            {
                int res = instructorService.ProcessPendingStatus(id, isApprove);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
