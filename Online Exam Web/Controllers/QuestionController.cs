using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using NuGet.Packaging;
using OnlineExam.DataAccess.data;
using OnlineExam.DataAccess.Repository;
using OnlineExam.DataAccess.Repository.IRepository;
using OnlineExam.Models;
using OnlineExam.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Data.SqlClient;

namespace Online_Exam_Web.Controllers
{
    [Authorize(Roles = "1")]
    public class QuestionController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public QuestionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string sortOrder)
        {
			ViewData["Subject"] = string.IsNullOrEmpty(sortOrder) ? "SubDesc" : "";
            List<Question> questions;
            switch (sortOrder)
            {
                case "SubDesc":
                    questions = _unitOfWork.QuesRepo.GetAll("Subject").OrderByDescending(e => e.SubjectId).ToList();
					break;
                default:
					questions = _unitOfWork.QuesRepo.GetAll("Subject").OrderBy(e => e.SubjectId).ToList();
					break;
			}
			return View(questions);
        }

        //get
        public IActionResult Create()
        {
            var questionVm = new QuestionVM()
            {
                question = new Question(),
                subjectList = _unitOfWork.SubRepo.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Subject_Name,
                    Value = i.Id.ToString(),
                    Disabled = !i.IsActive
                })
            };
            return View(questionVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(QuestionVM obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.QuesRepo.Add(obj.question);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }
				catch (Exception SqlException)
				{
					ModelState.AddModelError("SubjectId", "Atleast one subject has to be selected");
				}
			}
            return View(obj);
        }

        //get 
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var qAndOpBag = new OptionsVM()
            {
                question = new Question(),
                OptionsList = _unitOfWork.OptionRepo.GetAll().Where(u => u.QuestionId == id).ToList(),

            };
            var questionToEdit = _unitOfWork.QuesRepo.GetFirstOrDefault(u => u.Id == id);
            qAndOpBag.question = questionToEdit;

            return View(qAndOpBag);
        }

        [HttpPost]
        public IActionResult Edit(OptionsVM obj)
        {
            if (ModelState.IsValid)
            {
				List<Option> optionListFormView = new List<Option>();
                if(obj.OptionsList is not null)
                {
					foreach (var a in obj.OptionsList)
					{
						_unitOfWork.OptionRepo.update(a);
					}
				}
                
				//
				foreach (var option in obj.opTextList)
				{
					if (option is not null)
					{
						var opObj = new Option();
						opObj.option = option;
						opObj.QuestionId = obj.question.Id;
						optionListFormView.Add(opObj);
					}
				}
                _unitOfWork.OptionRepo.AddRange(optionListFormView);

				_unitOfWork.QuesRepo.Update(obj.question);
				_unitOfWork.Save();
				return RedirectToAction("Index");
				/*for (var i = 0; i < obj.opTextList.Count; i++)
				{
					if (obj.opTextList[i] is not null)
					{
						var opObj = new Option();
						opObj.option = obj.opTextList[i];
						opObj.QuestionId = obj.question.Id;
						optionListFormView.Add(opObj);
					}
				}*/
			}

			return View(obj);
		}

		//Get
		public IActionResult Delete(int? id)
        {
            if (id == null && id == 0)
            {
                return NotFound();
            }
            var qAndOpBag = new OptionsVM()
            {
                question = new Question(),
                OptionsList = _unitOfWork.OptionRepo.GetAll().Where(u => u.QuestionId == id).ToList(),
            };
            var questionToEdit = _unitOfWork.QuesRepo.GetFirstOrDefault(u => u.Id == id);
            qAndOpBag.question = questionToEdit;

            return View(qAndOpBag);
        }

        [HttpPost]
        public IActionResult Delete(OptionsVM obj)
        {
            _unitOfWork.QuesRepo.Delete(obj.question);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        //get 
        public IActionResult Answer(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var qAndOpBag = new OptionsVM()
            {
                question = new Question(),
                OptionsList = _unitOfWork.OptionRepo.GetAll().Where(u => u.QuestionId == id).ToList(),
                OptionsListSList = _unitOfWork.OptionRepo.GetAll().Where(i => i.QuestionId == id).Select(i => new SelectListItem
                {
                    Text = i.option,
                    Value = i.Id.ToString()
                })
            };
            var questionToEdit = _unitOfWork.QuesRepo.GetFirstOrDefault(u => u.Id == id);
            qAndOpBag.question = questionToEdit;

            return View(qAndOpBag);
        }

        [HttpPost]
        public IActionResult Answer(OptionsVM obj)
        {
            var addAns = _unitOfWork.QuesRepo.GetFirstOrDefault(e => e.Id == obj.question.Id);
            addAns.Answer = obj.question.Answer;
            _unitOfWork.QuesRepo.Update(addAns);
            _unitOfWork.Save();
            return RedirectToAction("Index");

            return View(obj);
        }

        public IActionResult Delete_op(int? id)
        {
            var deletOp = _unitOfWork.OptionRepo.GetFirstOrDefault(a => a.Id == id);
            _unitOfWork.OptionRepo.Delete(deletOp);
            _unitOfWork.Save();
			var Que = _unitOfWork.QuesRepo.GetFirstOrDefault(a => a.Id == deletOp.QuestionId);
			return RedirectToAction("Edit",new { id = Que.Id });
        }
    }
}
