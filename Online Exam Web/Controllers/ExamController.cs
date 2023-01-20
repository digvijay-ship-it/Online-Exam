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
using Newtonsoft.Json;
using System.Net;

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
	public IActionResult attemptedExamsList()
	{
		//make subject appear which are unattempted and 0
		var objList = unitOfWork.UserSub.GetAll("Subject").Where(e => e.UserId == Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) && e.Counter == 1);
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
    public IActionResult AttemptedWIthApi(int id)
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
				item.result.wasCurrect= 1;
			}
			else
			{
				item.result.wasCurrect = 0;
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
			return null;
		}

		var QuestionsList = unitOfWork.QuesRepo.GetAll().Where(e => e.SubjectId == id).ToList();
		var random = new Random();
		while (QuestionsList.Count() > 10)
		{
			QuestionsList.RemoveAt(random.Next(0, QuestionsList.Count()));
		}

		List<ExamVM> exam = new List<ExamVM>();

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
		/*var a = Json(new { examdata = exam });*/
		return Json(new { examdata = exam });
	}

	[HttpGet]
	public IActionResult GetExamResult(int? id ,int userId = 0)
	{
		/*id = 10;*/
		if (id == 0 || id is null)
		{
			return null;
		}
		if(userId== 0)
		{
			//triggers when user himself loged in
			userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
		}


		List<ExamVM> exam = new List<ExamVM>();
		var result = unitOfWork.ResultRepo.GetAll().Where(e => e.SubjectId == id && e.UserId == userId);
		/*so initially my result Model's column with name wasCurrect was a bool type and it stored
		 * bit type(0,1) of values so when itried to fetch data from data base it throw exception 
		 * as:--System.InvalidCastException: 'Unable to cast object of type 'System.String' to type 'System.Boolean'.'
		 * so my question is that previousely i have used few models and in those cases i have a 
		 * column called IsActive and it is bool type but in those cases i never got this error when
		 * I was doing crud on them so why did i got this error particularly for result model
		 */
		result = result.ToList();
		foreach (var item in result)
		{
			var questionSet = new ExamVM
			{
				result = item,
				OptionsList = unitOfWork.OptionRepo.GetAll("").Where(e => e.QuestionId == item.QuestionId).ToList(),
				question = unitOfWork.QuesRepo.GetFirstOrDefault(e => e.Id== item.QuestionId),
			};
			exam.Add(questionSet);
		}
		/*return RedirectToAction("AttemptWIthApi");*/
		/*var a = Json(new { examdata = exam });*/
		return Json(new { examdata = exam });
	}

	[HttpPost]
	public IActionResult AttemptApi(string DataInString)
	{
		/*Json ok = new Json();*/
		float percent=0;
		List<Result> results = JsonConvert.DeserializeObject<List<Result>>(DataInString);
		if (results is null|| results.Count==0)
		{
			return NotFound();
		}
		foreach (var item in results)
		{
			item.UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
			if (item.Answer == item.Users_Answer)
			{
				item.wasCurrect = 1;
				percent+=1;
			}
			else
			{
				item.wasCurrect = 0;
			}
		}

		int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

		//make that sub count to 1
		UserSubject UserSub = unitOfWork.UserSub.GetFirstOrDefault(u => u.UserId == userId && u.SubjectId == results[0].SubjectId);
		UserSub.Counter = 1;
		UserSub.percentage = ((float)percent/results.Count)*100;
		unitOfWork.UserSub.Update(UserSub);
		unitOfWork.ResultRepo.AddRange(results);
		unitOfWork.Save();

		return Ok();

    }
    #endregion

}
