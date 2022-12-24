using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineExam.Models;

namespace OnlineExam.Models.ViewModels
{
    public class QuestionVM
    {
        public Question question { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> subjectList { get; set; }
    }
}
