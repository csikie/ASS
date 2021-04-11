using ASS.BLL.Interfaces;
using ASS.Common.Enums;
using ASS.DAL;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ASS.BLL.Services
{
    public class AdminService : BaseService, IAdminService
    {
        public AdminService(ASSContext context, UserManager<User> userManager) : base(context, userManager) { }

        public void AddUserToSubject(string[] neptunCodes, string subjectName)
        {
            if (context.Subjects.Any(x => x.Name == subjectName))
            {
                throw new ArgumentException("Ez a tárgynév már foglalt!");
            }
            Subject subject = new Subject(subjectName);
            foreach (string neptunCode in neptunCodes)
            {
                User user = context.Users.FirstOrDefault(x => x.UserName == neptunCode);
                if (user != null && userManager.IsInRoleAsync(user, Role.Teacher.ToString()).Result)
                {
                    context.UserSubjects.Add(new UserSubject(subject, user));
                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<Subject> GetSubjects()
        {
            return context.Subjects.Include(s => s.UserSubject)
                                   .ThenInclude(u => u.User)
                                   .ToList();
        }

        public IEnumerable<User> GetUsers(Expression<Func<User, bool>> pred)
        {
            return context.Users.Where(pred);
        }

        public void UpdateSubject(int id, string subjectName, string[] teacherNeptunCodes)
        {
            Subject subject = context.Subjects.Include(s => s.UserSubject)
                                              .ThenInclude(u => u.User)
                                              .FirstOrDefault(x => x.Id == id);

            List<UserSubject> deletedTeachers = new List<UserSubject>();
            foreach (UserSubject teacher in subject.UserSubject)
            {
                if (!teacherNeptunCodes.Contains(teacher.User.UserName))
                {
                    deletedTeachers.Add(teacher);
                }
            }

            foreach (UserSubject deletedTeacher in deletedTeachers)
            {
                subject.UserSubject.Remove(deletedTeacher);
            }

            foreach (string teacherNeptunCode in teacherNeptunCodes)
            {
                if (!subject.UserSubject.Any(x => x.User.UserName == teacherNeptunCode))
                {
                    subject.UserSubject.Add(new UserSubject(subject, context.Users.FirstOrDefault(x => x.UserName == teacherNeptunCode)));
                }
            }

            string oldSubjectName = subject.Name;
            subject.Name = subjectName;

            context.Subjects.Update(subject);
            context.SaveChanges();

            if (context.Subjects.Where(x => x.Name == subjectName).Count() > 1)
            {
                subject.Name = oldSubjectName;
                context.Subjects.Update(subject);
                context.SaveChanges();

                throw new ArgumentException("A tárgynév módosítás nem sikerült mert az újonnan megadott tárgynév már foglalt!");
            }

        }

        public void DeleteSubject(int id)
        {
            Subject subject = context.Subjects.Include(s => s.UserSubject)
                                              .ThenInclude(u => u.User)
                                              .FirstOrDefault(x => x.Id == id);

            context.Subjects.Remove(subject);

            context.SaveChanges();
        }

        public List<User> GetAllUser()
        {
            return context.Users.Where(x => x.UserName != "admin")
                                .ToList();
        }

        public string[] GetUserRoles(int userId)
        {
            User user = context.Users.FirstOrDefault(x => x.Id == userId);

            return (userManager.GetRolesAsync(user)).Result.ToArray();
        }

        public void CreateUser(string realName, string userName, string email, string password, string[] roles)
        {
            User user = new User(userName, realName, email);
            IdentityResult createRes = userManager.CreateAsync(user, password).Result;
            if (!createRes.Succeeded)
            {
                throw new Exception($"Nem sikerült létrehozni a felhasználót! ({createRes})");
            }

            IdentityResult addToRoleRes = userManager.AddToRolesAsync(user, roles).Result;
            if (!addToRoleRes.Succeeded)
            {
                throw new Exception($"Nem sikerült létrehozni a felhasználót! ({addToRoleRes})");
            }
        }

        public void UpdateUser(int id, string userName, string realName, string email, string[] roles)
        {
            User user = context.Users.FirstOrDefault(x => x.Id == id);

            if (user.UserName != userName)
            {
                throw new Exception("A felhasználónév nem egyezik! A felhasználónevet nem lehet módosítani!");
            }

            user.RealName = realName;
            user.Email = email;

            IList<string> userRoles = userManager.GetRolesAsync(user).Result;
            List<string> deletedRoles = new List<string>();
            foreach (string userRole in userRoles)
            {
                if (!roles.Contains(userRole))
                {
                    deletedRoles.Add(userRole);
                }
            }

            IdentityResult roleDeleteRes = userManager.RemoveFromRolesAsync(user, deletedRoles).Result;
            if (!roleDeleteRes.Succeeded)
            {
                throw new Exception("Hiba történt a szerepkör(ök) eltávolítása során!");
            }

            foreach (string role in roles)
            {
                if (!userRoles.Contains(role))
                {
                    IdentityResult addRoleRes = userManager.AddToRoleAsync(user, role).Result;
                    if (!addRoleRes.Succeeded)
                    {
                        throw new Exception("Hiba történt a szerepkör(ök) hozzáadása során!");
                    }
                }
            }

            context.Update(user);
            context.SaveChanges();
        }
    }
}
