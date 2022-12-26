﻿using System.ComponentModel.DataAnnotations;

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

        public byte ExamCounter { get; set; } = 0;

        [Required]
        public int RoleId { get; set; } = 2;
        public Role Role { get; set; }
    }
}
