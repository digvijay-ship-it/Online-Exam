using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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

        [ValidateNever]
        public IList<UserSubject> UserSubjects { get; set; }
    }
}
