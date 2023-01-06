using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineExam.Models;

namespace OnlineExam.Models.ViewModels
{
    public class ExamVM
    {

        //OG
        public Question question { get; set; }

        [ValidateNever]
        public IList<Option> OptionsList { get; set; }

        [ValidateNever]
        public Result result { get; set; }

        [ValidateNever]
        public int? SubId { get; set; }
    }
}
