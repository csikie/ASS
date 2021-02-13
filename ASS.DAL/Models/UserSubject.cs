using System.ComponentModel.DataAnnotations;

namespace ASS.DAL.Models
{
    public class UserSubject
    {
        public UserSubject() { }
        public UserSubject(Subject subject, User user)
        {
            SubjectId = subject.Id;
            Subject = subject;
            UserId = user.Id;
            User = user;
        }
        [Key]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
