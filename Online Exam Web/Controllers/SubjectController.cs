using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.DataAccess.data;
using OnlineExam.DataAccess.Repository.IRepository;
using OnlineExam.Models;

namespace Online_Exam_Web.Controllers
{
    [Authorize(Roles ="1")]
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
            if(ModelState.IsValid)
            {
                try
                {
                    unitOfWork.SubRepo.Add(obj);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
                
                catch(Exception SqlException) 
                {
                    ModelState.AddModelError("Subject_Name", "Name alteady in use");
                }
            }
            return View(obj);
        }

        //get
        public IActionResult Edit(int? id)
        {
            if (id==0||id is null)
            {
                return NotFound();
            }
            var sub = unitOfWork.SubRepo.GetFirstOrDefault(u=>u.Id==id);
            if (sub==null)
            {
                return NotFound();
            } 
            return View(sub);
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
                try
                {
                    unitOfWork.SubRepo.Update(obj);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }

                catch (Exception SqlException)
                {
                    ModelState.AddModelError("Subject_Name", "Name alteady in use");
                }
            }
            return View(obj);
        }

        //get
        public IActionResult Delete(int? id)
        {
            if(id==0||id is null)
            {
                return NotFound();
            }
            var delsub = unitOfWork.SubRepo.GetFirstOrDefault(u=>u.Id== id);
            if(delsub is null)
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
