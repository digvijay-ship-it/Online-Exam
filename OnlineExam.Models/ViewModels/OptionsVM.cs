using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineExam.Models;

namespace OnlineExam.Models.ViewModels
{
    public class OptionsVM
    {
        public Question question { get; set; }

        public IEnumerable<Option> OptionsList { get; set; }
    }
}
