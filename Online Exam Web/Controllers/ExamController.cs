using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OnlineExam.DataAccess.data;
using OnlineExam.DataAccess.Repository.IRepository;
using OnlineExam.Models;
using OnlineExam.Models.ViewModels;

namespace Online_Exam_Web.Controllers;

public class ExamController : Controller
{
	public readonly IUnitOfWork unitOfWork;

	public ExamController(IUnitOfWork nitOfWork)
	{
		unitOfWork = nitOfWork;
	}

	public IActionResult Index()
	{
		//make subject appear which are unattempted and 0
		var objList = unitOfWork.UserSub.GetAll("Subject").Where(e => e.UserId == Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) && e.Counter == 0);
		return View(objList.ToList());
	}

	//get
	public IActionResult Attempt(int? id)
	{
		//id is from subjectId
		if (id == 0 || id is null)
		{
			return NotFound();
		}

		var QuestionsList = unitOfWork.QuesRepo.GetAll("Subject").Where(e => e.SubjectId == id).ToList();
		var random = new Random();
		while (QuestionsList.Count() > 2)
		{
			QuestionsList.RemoveAt(random.Next(0, QuestionsList.Count()));
		}

		IList<ExamVM> exam = new List<ExamVM>();

		foreach (var item in QuestionsList)
		{
			var questionSet = new ExamVM
			{
				question = item,
				OptionsList = unitOfWork.OptionRepo.GetAll().Where(e => e.QuestionId == item.Id).ToList(),
				result = new ()
			};
			exam.Add(questionSet);
		}
		return View(exam);
	}
	public IActionResult AttemptWIthApi(int id)
	{
		if (id == 0)
		{
			return NotFound();
		}
		var sub = unitOfWork.SubRepo.GetFirstOrDefault(x => x.Id == id);
		return View(sub);
	}

	//Post
	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Attempt(string idsa)
	{
		/*Json ok = new Json();*/
		List <ExamVM> ids = new List<ExamVM>();
		if (ids is null)
		{
			return NotFound();
		}
		List<Result> resultList = new List<Result>(); 
		foreach(var item in ids)
		{
			item.result.UserId =Convert.ToInt32( User.FindFirstValue(ClaimTypes.NameIdentifier));
			if (item.result.Answer == item.result.Users_Answer)
			{
				item.result.wasCurrect= true;
			}
			else
			{
				item.result.wasCurrect = false;
			}
			resultList.Add(item.result);
		}
		int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
		
		//make that sub count to 1
		UserSubject UserSub = unitOfWork.UserSub.GetFirstOrDefault(u => u.UserId == userId && u.SubjectId == ids[0].result.SubjectId);
		UserSub.Counter = 1;
		unitOfWork.UserSub.Update(UserSub);
		unitOfWork.ResultRepo.AddRange(resultList);
		unitOfWork.Save();

		return View();

	}


	#region API CALLS
	[HttpGet]
	public IActionResult GetAllExam(int? id)
	{
		/*id = 10;*/
		if (id == 0 || id is null)
		{
			return NotFound();
		}

		var QuestionsList = unitOfWork.QuesRepo.GetAll().Where(e => e.SubjectId == id).ToList();
		var random = new Random();
		while (QuestionsList.Count() > 2)
		{
			QuestionsList.RemoveAt(random.Next(0, QuestionsList.Count()));
		}

		IList<ExamVM> exam = new List<ExamVM>();

		foreach (var item in QuestionsList)
		{
			var questionSet = new ExamVM
			{
				question = item,
				OptionsList = unitOfWork.OptionRepo.GetAll().Where(e => e.QuestionId == item.Id).ToList(),
				result = new(),
			};
			exam.Add(questionSet);
		}
		/*return RedirectToAction("AttemptWIthApi");*/
		return Json(new { examdata = exam });

	}
	[HttpPost]
	public IActionResult AttemptApi(List<ExamVM> ids)
	{
		/*Json ok = new Json();*/
		if (ids is null)
		{
			return NotFound();
		}
		List<Result> resultList = new List<Result>();
		foreach (var item in ids)
		{
			item.result.UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
			if (item.result.Answer == item.result.Users_Answer)
			{
				item.result.wasCurrect = true;
			}
			else
			{
				item.result.wasCurrect = false;
			}
			resultList.Add(item.result);
		}
		int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

		//make that sub count to 1
		UserSubject UserSub = unitOfWork.UserSub.GetFirstOrDefault(u => u.UserId == userId && u.SubjectId == ids[0].result.SubjectId);
		UserSub.Counter = 1;
		unitOfWork.UserSub.Update(UserSub);
		unitOfWork.ResultRepo.AddRange(resultList);
		unitOfWork.Save();

		return View();

	}
	#endregion

}
