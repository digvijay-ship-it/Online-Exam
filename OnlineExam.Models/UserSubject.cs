using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Models
{
    public class UserSubject
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int Counter { get; set; } = 0;
    }
}
