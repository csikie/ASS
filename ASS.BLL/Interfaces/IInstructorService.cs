using ASS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASS.BLL.Interfaces
{
    public interface IInstructorService
    {
        int ProcessPendingStatus(int id, bool isApprove);
        Task<List<UserCourse>> GetPendingList(ClaimsPrincipal user); 
        Task<List<Course>> GetCourses(ClaimsPrincipal user);
        void CreateAssignment(string name, string desc, DateTime startDate, DateTime endDate, int[] courseIds);
        Task<Assignment> GetAssignment(int courseId, int assignmentId, ClaimsPrincipal user);
        User GetStudent(int studentId);
    }
}
