using ASS.Common.Enums;
using ASS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ASS.BLL.Interfaces
{
    public interface IAdminService
    {
        void CreateSubject(string[] neptunCodes, string subjectName);
        IEnumerable<Subject> GetSubjects();
        void UpdateSubject(int id, string subjectName, string[] teacherNeptunCodes);
        void DeleteSubject(int id);
        public List<User> GetAllUser();
        public string[] GetUserRoles(int userId);
        void CreateUser(string realName, string userName, string email, string password, string[] roles);
        void UpdateUser(int id, string userName, string realName, string email, string[] roles);
    }
}
