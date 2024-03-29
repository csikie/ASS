﻿using ASS.BLL.Services;
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
        [ValidateAntiForgeryToken]
        public IActionResult CourseRegistration(CourseRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                studentService.CourseRegistration(model.CourseIds, User);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<string> Read_AssignmentGrid()
        {
            IEnumerable<StudentCoursesDTO> studentAssignments = (await studentService.Read_AssignmentGrid(User)).Select(x => new StudentCoursesDTO(x.CourseId,
                                                                                                                                                   x.Course.Name,
                                                                                                                                                   x.Course.Assignments.Where(y => y.StartDate <= DateTime.Now)
                                                                                                                                                                       .Select(y => new AssignmentDTO(y.Id, y.Name, y.Description, y.StartDate, y.EndDate)
                                                                                                                                                                                   {
                                                                                                                                                                                        Solutions = y.Solutions.Where(z => z.EvaluationTime == y.Solutions.Max(v => v.EvaluationTime) && z.Grade != null)
                                                                                                                                                                                                               .Select(z => new SolutionDTO() { Grade = z.Grade })
                                                                                                                                                                                                               .ToList()
                                                                                                                                                                                   })
                                                                                                                                                                       .ToArray()));
            string result = JsonConvert.SerializeObject(studentAssignments);
            return result;
        }

        public async Task<IActionResult> Assignment(int id)
        {
            var assignment = (await studentService.GetAssignment(id, User));
            AssignmentDTO assignmentDTO = new AssignmentDTO(assignment.Id, assignment.Name, assignment.Description, assignment.StartDate, assignment.EndDate);
            if (assignment.Solutions != null)
            {
                assignmentDTO.Solutions = assignment.Solutions.Select(x => new SolutionDTO(x.Id,x.SubmittedSolution,x.SubmissionTime,x.Grade,x.EvaluationTime)).ToList();
            }
            return View(assignmentDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitSolution(SolutionSubmissionViewModel solution)
        {
            if (ModelState.IsValid)
            {
                studentService.SubmitSolution(solution.Id, User, solution.SubmittedSolution, DateTime.Now);
                return RedirectToAction("Assignment", new { id = solution.Id});
            }
            return RedirectToAction("Assignment", solution.Id);
        }
    }
}
