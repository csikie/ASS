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

        public async Task<IActionResult> Index()
        {
            List<CourseDTO> model = (await instructorService.GetCourses(User)).Select(x => new CourseDTO(x.Id,x.Name,null,x.Subject.Name)
                                                                                            {
                                                                                                Assignments = x.Assignments.Select(y => new AssignmentDTO(y.Id, y.Name, y.Description, y.StartDate, y.EndDate)
                                                                                                                                        {
                                                                                                                                            Solutions = y.Solutions.Select(z => new SolutionDTO(z.Id,z.SubmittedSolution,z.SubmissionTime,z.Grade,z.EvaluationTime))
                                                                                                                                                                   .ToList()
                                                                                                                                        })
                                                                                                                            .ToList(),
                                                                                                Students = x.UserCourses.Where(y => y.CourseId == x.Id)
                                                                                                                        .Select(y => new UserDTO(y.User.RealName, y.User.UserName, null, null)
                                                                                                                                     {
                                                                                                                                        Id = y.UserId,
                                                                                                                                        Solutions = y.User.Solutions.Select(z => new SolutionDTO(z.Id, z.SubmittedSolution, z.SubmissionTime, z.Grade, z.EvaluationTime))
                                                                                                                                                                    .ToList()
                                                                                                                        })
                                                                                                                        .ToList()
                                                                                            }).ToList();
            return View(model);
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

        public async Task<IActionResult> EvaluateAssignment(int course, int assignment, int student)
        {
            var assignmentObject = await instructorService.GetAssignment(course, assignment, User);
            var studentObject = instructorService.GetStudent(student);
            AssignmentDTO assignmentDTO = new AssignmentDTO(assignmentObject.Id, assignmentObject.Name, assignmentObject.Description, assignmentObject.StartDate, assignmentObject.EndDate)
                                  {
                                        Solutions = assignmentObject.Solutions.Where(x => x.UserId == student)
                                                                              .Select(x => new SolutionDTO(x.Id,x.SubmittedSolution,x.SubmissionTime,x.Grade,x.EvaluationTime))
                                                                              .OrderByDescending(x => x.SubmissionTime)
                                                                              .ToList()
                                  };
            EvaluateAssignmentDTO model = new EvaluateAssignmentDTO
            {
                Assignment = assignmentDTO,
                StudentName = $"{studentObject.RealName} ({studentObject.UserName})"
            }; 
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EvaluateAssignment(int solutionId, string grade)
        {
            instructorService.EvaluateAssignment(solutionId, grade, DateTime.Now, User);

            return RedirectToAction("Index");
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
