using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OnlineExam.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string question { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public int SubjectId { get; set; }
        [ValidateNever]
        public Subject Subject { get; set; }

        [Required]
        public int Answer { get; set; }
    }
}
