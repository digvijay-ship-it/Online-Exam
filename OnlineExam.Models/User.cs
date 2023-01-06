using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OnlineExam.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public int RoleId { get; set; } = 2;
        public Role Role { get; set; }

        [ValidateNever]
        public IList<UserSubject> UserSubjects { get; set; }
    }
}
