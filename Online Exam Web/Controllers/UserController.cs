using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.DataAccess.data;
using OnlineExam.DataAccess.Repository.IRepository;
using OnlineExam.Models;
using OnlineExam.Models.ViewModels;

namespace Online_Exam_Web.Controllers;
[Authorize(Roles = "1")]
public class UserController : Controller
{
	public readonly IUnitOfWork unitOfWork;

	public UserController(IUnitOfWork nitOfWork)
	{
		unitOfWork = nitOfWork;
	}

	public IActionResult Index()
	{
		var objList = unitOfWork.UserRepo.GetAll();

		return View(objList);
	}

	//get
	public IActionResult Edit(int? id)
	{
		if (id == 0 || id is null)
		{
			return NotFound();
		}
		var user = unitOfWork.UserRepo.GetFirstOrDefault(u => u.Id == id);
		if (user == null)
		{
			return NotFound();
		}
		return View(user);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Edit(User obj)
	{
		var user = unitOfWork.UserRepo.GetFirstOrDefault(u => u.Id == obj.Id);
		user.IsActive = obj.IsActive;
		unitOfWork.UserRepo.Update(user);
		unitOfWork.Save();
		return RedirectToAction("Index");

		return View(obj);
	}

	public IActionResult UserExamDetails(int? id)
	{
		//make subject appear which are attempted or one
		var objList = unitOfWork.UserSub.GetAll("Subject").Where(e => e.UserId == id && e.Counter != 0);
		return View(objList.ToList());
	}
	#region Api calls
	[HttpGet]
	public IActionResult GetExamResultByAdmin(int? id, int uId = 0)
	{
		/*id = 10;*/
		if (id == 0 || id is null)
		{
			return null;
		}
		if (uId == 0)
		{
			//triggers when user himself loged in
			uId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
		}


		List<ExamVM> exam = new List<ExamVM>();
		var result = unitOfWork.ResultRepo.GetAll().Where(e => e.SubjectId == id && e.UserId == uId);
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
				question = unitOfWork.QuesRepo.GetFirstOrDefault(e => e.Id == item.QuestionId),
			};
			exam.Add(questionSet);
		}
		/*return RedirectToAction("AttemptWIthApi");*/
		/*var a = Json(new { examdata = exam });*/
		//return Json(new { examdata = exam });
		return Json(new { examdata = exam });

	}
	#endregion
}