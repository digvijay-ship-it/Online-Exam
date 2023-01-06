using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.DataAccess.data;
using OnlineExam.DataAccess.Repository.IRepository;
using OnlineExam.Models;

namespace Online_Exam_Web.Controllers
{
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

    }
}
