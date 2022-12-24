using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineExam.Models
{
    public class Option
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string option { get; set; }

        [Required]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
