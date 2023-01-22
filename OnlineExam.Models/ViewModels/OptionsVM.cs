using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineExam.Models;

namespace OnlineExam.Models.ViewModels
{
    public class OptionsVM
    {

        //OG
        public Question question { get; set; }

        [ValidateNever]
        public List<Option> OptionsList { get; set; }

		[ValidateNever]
		public List<string> opTextList { get; set; }


		[ValidateNever]
        public IEnumerable<SelectListItem> OptionsListSList { get; set; }


        [ValidateNever]
        public Result result { get; set; }
    }
}
