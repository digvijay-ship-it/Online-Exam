using Microsoft.AspNetCore.Mvc;
using OnlineExam.DataAccess.data;
using OnlineExam.DataAccess.Repository.IRepository;
using OnlineExam.Models;

namespace Online_Exam_Web.Controllers
{
    public class ExamController : Controller
    {
        public readonly IUnitOfWork unitOfWork;
        
        public ExamController(IUnitOfWork nitOfWork)
        {
            unitOfWork = nitOfWork;
        }

        public IActionResult Index()
        {
            var objList = unitOfWork.SubRepo.GetAll().Where(e=>e.IsActive==true);
            return View(objList);
        }
    }
}
