using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineExam.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Subject Name")]
        public string Subject_Name { get; set; }

        public bool IsActive { get; set; }
    }
}
