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
    }
}
