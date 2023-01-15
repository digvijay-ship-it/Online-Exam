using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.DataAccess.data;
using OnlineExam.DataAccess.Repository;
using OnlineExam.DataAccess.Repository.IRepository;
using OnlineExam.Models;

namespace Online_Exam_Web.Controllers
{
	[Authorize(Roles = "1")]
	public class SubjectController : Controller
	{
		public readonly IUnitOfWork unitOfWork;

		public SubjectController(IUnitOfWork nitOfWork)
		{
			unitOfWork = nitOfWork;
		}

		public IActionResult Index()
		{
			var objList = unitOfWork.SubRepo.GetAll();

			return View(objList);
		}
		//get
		public IActionResult Create()
		{
			return View();
		}
		//post
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Subject obj)
		{
			if (ModelState.IsValid)
			{
				//cheak if it is unique
				var subNameNotUnique = unitOfWork.SubRepo.GetFirstOrDefaultBool(s => s.Subject_Name == obj.Subject_Name);
				if (subNameNotUnique)
				{
					ModelState.AddModelError("Subject_Name", "Subject name already Exist");
					return View(obj);
				}
				unitOfWork.SubRepo.Add(obj);
				unitOfWork.Save();
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		//get
		public IActionResult Edit(int? id)
		{
			if (id == 0 || id is null)
			{
				return NotFound();
			}
			var sub = unitOfWork.SubRepo.GetFirstOrDefault(u => u.Id == id);
			if (sub == null)
			{
				return NotFound();
			}
			return View(sub);
		}

		[AcceptVerbs("Post", "Get")]
		public IActionResult SubIsExist(string sub)
		{
			var data = unitOfWork.SubRepo.GetFirstOrDefault(e => e.Subject_Name == sub);
			if (data != null)
			{
				return Json($"Email{sub} already in Use");
			}
			else
			{
				return Json(true);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Subject obj)
		{
			if (ModelState.IsValid)
			{
				/*bool notchnaged = unitOfWork.SubRepo.GetFirstOrDefaultBool(u=>u.Id==obj.Id&&u.Subject_Name==obj.Subject_Name);
				if(notchnaged)
				{

				}*/
				//cheak if object is enven changed
				var objNotChanged = unitOfWork.SubRepo.GetFirstOrDefaultBool(s => s.Id == obj.Id && s.Subject_Name == obj.Subject_Name);

				if (objNotChanged)
				{
                    unitOfWork.SubRepo.Update(obj);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
				else
				{
					//so name is changed
					//now cheak if it is unique
					var subNameNotUnique = unitOfWork.SubRepo.GetFirstOrDefaultBool(s => s.Subject_Name == obj.Subject_Name);
					if (subNameNotUnique)
					{
						ModelState.AddModelError("Subject_name", "sub Name Already Exist");
						return View(obj);
					}
					else
					{
                        unitOfWork.SubRepo.Update(obj);
                        unitOfWork.Save();
                        return RedirectToAction("Index");
                    }
				}
			}


			/*ModelState.AddModelError("Subject_Name", "Name alteady in use");*/
			return View(obj);
		}

		//get
		public IActionResult Delete(int? id)
		{
			if (id == 0 || id is null)
			{
				return NotFound();
			}
			var delsub = unitOfWork.SubRepo.GetFirstOrDefault(u => u.Id == id);
			if (delsub is null)
			{
				return NotFound();
			}
			return View(delsub);
		}
		//
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(Subject obj)
		{
			unitOfWork.SubRepo.Delete(obj);
			unitOfWork.Save();
			return RedirectToAction("Index");
		}
	}
}
