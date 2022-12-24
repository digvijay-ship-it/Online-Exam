using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using OnlineExam.DataAccess.data;
using OnlineExam.DataAccess.Repository;
using OnlineExam.DataAccess.Repository.IRepository;
using OnlineExam.Models;
using OnlineExam.Models.ViewModels;

namespace Online_Exam_Web.Controllers
{
    public class QuestionController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public QuestionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Question> questions = _unitOfWork.QuesRepo.GetAll();
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
                _unitOfWork.QuesRepo.Add(obj.question);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //get 
        public IActionResult Edit(int? id)
        {
            if(id == null&&id==0) 
            {
                return NotFound();
            }
            var qAndOpBag = new OptionsVM()
            {
                question = new Question(),
                OptionsList = _unitOfWork.OptionRepo.GetAll().Where(u=>u.QuestionId == id),
            };
            var questionToEdit = _unitOfWork.QuesRepo.GetFirstOrDefault(u=>u.Id == id);
            qAndOpBag.question = questionToEdit;

            return View(qAndOpBag);
        }

        [HttpPost]
        public IActionResult Edit(OptionsVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.QuesRepo.Update(obj.question);
                _unitOfWork.OptionRepo.AddRange(obj.OptionsList.ToArray());
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
