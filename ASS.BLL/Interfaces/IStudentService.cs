using ASS.Common.Enums;
using ASS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASS.BLL.Interfaces
{
    public interface IStudentService
    {
        Task<List<Course>> GetCourses(ClaimsPrincipal user);
        void CourseRegistration(int[] courseIds, ClaimsPrincipal user);
        Task<List<UserCourse>> Read_AssignmentGrid(ClaimsPrincipal user);
        Task<Assignment> GetAssignment(int assignmentId, ClaimsPrincipal user);
    }
}
