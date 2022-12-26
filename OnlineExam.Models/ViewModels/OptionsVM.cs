using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineExam.Models;

namespace OnlineExam.Models.ViewModels
{
    public class OptionsVM
    {
        public Question question { get; set; }

        [ValidateNever]
        public IEnumerable<Option> OptionsList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> OptionsListSList { get; set; }

        [ValidateNever]
        public string Option1 { get; set; }
        [ValidateNever]
        public string Option2 { get; set; }
        [ValidateNever]
        public string Option3 { get; set; }
        [ValidateNever]
        public string Option4 { get; set; }
    }
}
