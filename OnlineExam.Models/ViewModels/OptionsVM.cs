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
        public IList<Option> OptionsList { get; set; }

		[ValidateNever]
		public List<string> opTextList { get; set; }

        [ValidateNever] 
		public string opText { get; set; }


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

        [ValidateNever]
        public Result result { get; set; }
    }
}
