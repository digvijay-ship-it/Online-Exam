using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

		[Required]
		public int SubjectId { get; set; }
		public Subject Subject { get; set; }

		[Required]
        public int QuestionId { get; set; } 
        public Question Question { get; set; }

        [Required]
        public int Answer { get; set; }

        public int? Users_Answer { get; set; }

        [Required]
        public bool wasCurrect { get; set; }

        public DateTime date { get; set; } = DateTime.Now;
    }
}