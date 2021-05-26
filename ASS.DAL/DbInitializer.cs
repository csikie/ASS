using ASS.Common.Enums;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ASS.DAL
{
    public static class DbInitializer
    {
        private static ASSContext context;

        public static void Initialize(IServiceProvider serviceProvider, UserManager<User> userManager, string appMode)
        {
            context = serviceProvider.GetRequiredService<ASSContext>();
            if (appMode == "Test")
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                CreateSystemTestData(userManager);
            }
            else
            {
                context.Database.EnsureCreated();
                if (!context.Users.Any())
                {
                    CreateSystemData(userManager);
                }
            }
        }

        private async static void CreateSystemBaseData(UserManager<User> userManager)
        {
            foreach (Role item in Enum.GetValues(typeof(Role)))
            {
                context.Roles.Add(new IdentityRole<int>() 
                                      {     
                                            Name = item.ToString(),
                                            NormalizedName = item.ToString() 
                                      });
            }

            await CreateUser("admin", "Teszt Elek", "admin@inf.elte.hu", "Ab1234", Role.Admin, userManager);
        }

        private async static Task<User> CreateUser(string userName, string realName, string email, string password, Role role, UserManager<User> userManager)
        {
            User user = new User(userName, realName, email);
            IdentityResult res = await userManager.CreateAsync(user, password);
            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role.ToString());
            }
            return user;
        }

        private async static void CreateSystemTestData(UserManager<User> userManager)
        {
            CreateSystemBaseData(userManager);
            User teacher = await CreateUser("teacher", "Teszt Elek", "teacher@inf.elte.hu", "Ab1234", Role.Teacher, userManager);
            User instructor = await CreateUser("instructor", "Teszt Elek", "instructor@inf.elte.hu", "Ab1234", Role.Instructor, userManager);
            User student = await CreateUser("student", "Teszt Elek", "student@inf.elte.hu", "Ab1234", Role.Student, userManager);

            Subject funkcProg = new Subject("Funkcionális programozás EA+GY");
            context.UserSubjects.Add(new UserSubject(funkcProg, teacher));
            context.SaveChanges();

            Subject funkcNyelvek = new Subject("Funkcionális nyelvek EA+GY");
            context.UserSubjects.Add(new UserSubject(funkcNyelvek, teacher));
            context.SaveChanges();

            Course course = new Course("Gyak #1", funkcProg);
            context.Instructors.Add(new Instructors(course, instructor));
            context.SaveChanges();

            Course course2 = new Course("Gyak #2", funkcNyelvek);
            context.Instructors.Add(new Instructors(course2, instructor));
            context.SaveChanges();

            Course tempCourse = new Course("Gyak Temp", funkcNyelvek);
            context.Instructors.Add(new Instructors(tempCourse, instructor));
            context.SaveChanges();

            Course tempCourse2 = new Course("Gyak Temp#2", funkcNyelvek);
            context.Instructors.Add(new Instructors(tempCourse2, instructor));
            context.SaveChanges();

            context.UserCourse.Add(new UserCourse(course2, student) { Pending = true });
            context.SaveChanges();

            context.UserCourse.Add(new UserCourse(course, student));
            context.SaveChanges();

            context.UserCourse.Add(new UserCourse(tempCourse, student));
            context.SaveChanges();

            Assignment assignment = new Assignment("Teszt feladat", "Teszt leírás", DateTime.Now, DateTime.Now.AddDays(5), course2);
            context.Assignments.Add(assignment);
            context.SaveChanges();

            context.Solutions.Add(new Solution("Teszt megoldás2.", DateTime.Now, assignment, student));
            context.SaveChanges();
        }

        private async static void CreateSystemData(UserManager<User> userManager)
        {
            CreateSystemBaseData(userManager);

            User student = await CreateUser("student", "Teszt Elek", "student@inf.elte.hu", "Ab1234", Role.Student, userManager);

            User instructorTeacher = new User("poora", "Poór Artúr", "poora@inf.elte.hu");
            IdentityResult res3 = await userManager.CreateAsync(instructorTeacher, "Ab1234");
            await userManager.AddToRoleAsync(instructorTeacher, Role.Teacher.ToString());
            await userManager.AddToRoleAsync(instructorTeacher, Role.Instructor.ToString());

            User user4 = await CreateUser("csikie", "Csiki Erik Gergely", "csikie@inf.elte.hu", "Ab1234", Role.Instructor, userManager);

            User user5 = await CreateUser("bozo_i", "Bozó István", "bozo_i@inf.elte.hu", "Ab1234", Role.Teacher, userManager);

            Subject subject = new Subject("Funkcionális programozás EA+GY");
            context.UserSubjects.Add(new UserSubject(subject, instructorTeacher));
            context.UserSubjects.Add(new UserSubject(subject, user5));
            context.SaveChanges();

            Subject subject2 = new Subject("Funkcionális nyelvek EA+GY");
            context.UserSubjects.Add(new UserSubject(subject2, instructorTeacher));
            context.SaveChanges();

            Course course = new Course("Gyak #1", subject);
            context.Instructors.Add(new Instructors(course, user4));
            context.SaveChanges();

            Course course2 = new Course("Gyak #2", subject2);
            context.Instructors.Add(new Instructors(course2, user4));
            context.SaveChanges();

            context.UserCourse.Add(new UserCourse(course2, student));
            context.SaveChanges();


        }
    }
}
