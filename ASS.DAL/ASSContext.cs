using ASS.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASS.DAL
{
    public class ASSContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourse { get; set; }
        public DbSet<Instructors> Instructors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<UserSubject> UserSubjects { get; set; }

        public ASSContext(DbContextOptions<ASSContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            int stringMaxLength = 200;
            // User IdentityRole and IdentityUser in case you haven't extended those classes
            builder.Entity<IdentityRole<int>>(x => x.Property(m => m.Name).HasMaxLength(stringMaxLength));
            builder.Entity<IdentityRole<int>>(x => x.Property(m => m.NormalizedName).HasMaxLength(stringMaxLength));
            builder.Entity<User>(x => x.Property(m => m.NormalizedUserName).HasMaxLength(stringMaxLength));

            // We are using int here because of the change on the PK
            builder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(stringMaxLength));
            builder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.ProviderKey).HasMaxLength(stringMaxLength));

            // We are using int here because of the change on the PK
            builder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(stringMaxLength));
            builder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.Name).HasMaxLength(stringMaxLength));
            // A felhasználói tábla alapértelmezett neve AspNetUsers lenne az adatbázisban, de ezt felüldefiniálhatjuk.

            builder.Entity<UserCourse>()
                   .HasKey(bc => new { bc.UserId, bc.CourseId });
            builder.Entity<UserCourse>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserCourses)
                .HasForeignKey(bc => bc.UserId);
            builder.Entity<UserCourse>()
                .HasOne(bc => bc.Course)
                .WithMany(c => c.UserCourses)
                .HasForeignKey(bc => bc.CourseId);

            builder.Entity<Instructors>()
                   .HasKey(bc => new { bc.UserId, bc.CourseId });
            builder.Entity<Instructors>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.Instructors)
                .HasForeignKey(bc => bc.UserId);
            builder.Entity<Instructors>()
                .HasOne(bc => bc.Course)
                .WithMany(c => c.Instructors)
                .HasForeignKey(bc => bc.CourseId);

            builder.Entity<UserSubject>()
                   .HasKey(bc => new { bc.UserId, bc.SubjectId });
            builder.Entity<UserSubject>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserSubject)
                .HasForeignKey(bc => bc.UserId);
            builder.Entity<UserSubject>()
                .HasOne(bc => bc.Subject)
                .WithMany(c => c.UserSubject)
                .HasForeignKey(bc => bc.SubjectId);

            builder.Entity<Course>()
                   .HasOne<Subject>(s => s.Subject)
                   .WithMany(g => g.Courses)
                   .HasForeignKey(s => s.SubjectId);

            builder.Entity<UserSubject>()
                   .HasKey(k => k.Id);

            builder.Entity<Course>()
                   .HasKey(k => k.Id);

            builder.Entity<Subject>()
                   .HasKey(k => k.Id);

            builder.Entity<Instructors>()
                   .HasKey(k => k.Id);
        }
    }
}
