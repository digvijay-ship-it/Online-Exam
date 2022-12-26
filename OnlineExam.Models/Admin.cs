using System.ComponentModel.DataAnnotations;

namespace OnlineExam.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public int RoleId { get; set; } = 1;
        public Role Role { get; set; }
    }
}
