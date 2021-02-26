using ASS.Common.Enums;
using ASS.DAL;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ASS.BLL.Services
{
    public class AdminService : BaseService, IAdminService
    {
        public AdminService(ASSContext context, UserManager<User> userManager) : base(context, userManager) { }

        public async void AddUserToSubject(string[] neptunCodes, string subjectName)
        {
            Subject subject = new Subject(subjectName);
            foreach (string neptunCode in neptunCodes)
            {
                User user = context.Users.FirstOrDefault(x => x.UserName == neptunCode);
                if (user != null && await userManager.IsInRoleAsync(user, Role.Teacher.ToString()))
                {
                    context.UserSubjects.Add(new UserSubject(subject, user));
                    context.SaveChanges();
                    //return true;
                }
            }
            //return false;
        }

        public string GetFullName(string username) => FullName(username);

        public IEnumerable<Subject> GetSubjects()
        {
            return context.Subjects.Include(s => s.UserSubject)
                                   .ThenInclude(u => u.User)
                                   .ToList();
        }

        public async Task<IEnumerable<User>> GetUsersInRole(Role role)
        {
            return (await userManager.GetUsersInRoleAsync(role.ToString())).ToList();
        }

        public IEnumerable<User> GetUsers(Expression<Func<User,bool>> pred)
        {
            return context.Users.Where(pred);
        }

        public async void UpdateSubject(int id, string subjectName, string[] teacherNeptunCodes)
        {
            Subject subject = context.Subjects.Include(s => s.UserSubject).ThenInclude(u => u.User).FirstOrDefault(x => x.Id == id);

            subject.Name = subjectName;
            List<User> users = (await userManager.GetUsersInRoleAsync(Role.Teacher.ToString())).Where(x => teacherNeptunCodes.Contains(x.UserName)).ToList();
            List<UserSubject> teachers = new List<UserSubject>();

            foreach (User user in users)
            {
                teachers.Add(new UserSubject(subject,user));
            }

            subject.UserSubject = teachers;

            context.Subjects.Update(subject);
            context.SaveChanges();
        }
    }
}
