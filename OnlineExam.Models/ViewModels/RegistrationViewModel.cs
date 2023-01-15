using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineExam.Models.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Remote(action: "EmailIsExist",controller: "Account")]
		[RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter Valid Email")]
		public string Email { get; set; }
        
        [Required]
		[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$" , ErrorMessage = "Minimum eight characters long ,including at least one letter and one number And No Speacial character!!!")]
		public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Remember Me")]
        public bool IsRemember { get; set; }

        [ValidateNever]
        public IList<SelectListItem>? SubjectList { get; set; }
    }
}
