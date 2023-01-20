using Microsoft.AspNetCore.Mvc;
using OnlineExam.DataAccess.Repository.IRepository;
using OnlineExam.Models;

namespace Online_Exam_Web.Controllers;
public class ResultController : Controller
{
    public readonly IUnitOfWork unitOfWork;

    public ResultController(IUnitOfWork nitOfWork)
    {
        unitOfWork = nitOfWork;
    }

    public IActionResult Index()//DateTime? searchString1, DateTime? searchString2
    {
        /*ViewData["StartDateFilter"] = searchString1;
        ViewData["EndDateFilter"] = searchString2;*/

        var resultList = unitOfWork.ResultRepo.GetAll("User,Subject,Question");
        return View(resultList);
    }

    [HttpGet]
    public IActionResult ResultGropupdataShow(DateTime? searchString1, DateTime? searchString2)
    {
        var resultList = unitOfWork.ResultRepo.GetAll("User,Subject,Question");

        //send that data in view with 
        if (searchString1 is not null||searchString2 is not null)
        {
            resultList = unitOfWork.ResultRepo.GetAll().Where(s => s.date >= searchString1).ToList();
            if (searchString2 is not null)
            {
                resultList = unitOfWork.ResultRepo.GetAll().Where(s => s.date <= searchString2).ToList();
            }
            //perform group by on Result.UserId,Result.SubjectId and aggragate in IsCurrect as percentage
            //and use links to display viewresults in details
            /*
                            var query = unitOfWork.ResultRepo.GetAll().GroupBy(e => new { e.UserId, e.SubjectId }).Select(s=>  new Result()
                            {
                                UserId = s.	
                            }).ToList();*/
            var query = from R in resultList
                        group R by new
                        {   R.UserId,
                            R.User.Email,
                            R.SubjectId,
                            R.Subject.Subject_Name
                        } into resultGroup
                        orderby resultGroup.Key.Email ascending
                        orderby resultGroup.Key.Subject_Name ascending
                        select new
                        {
                            UserId = resultGroup.Key.UserId,
                            User_Email = resultGroup.Key.Email,
                            subjectId = resultGroup.Key.SubjectId,
                            subject_Name = resultGroup.Key.Subject_Name,
                            percentage = (Convert.ToDecimal(resultGroup.Sum(e => e.wasCurrect)) / ( resultGroup.Count())) * 100,
                            results = resultGroup.OrderBy(s => s.UserId)
                        };




            return Json(new { data=query });
            /*return RedirectToAction("ResultGropupdataShow", query);*/
        }
        return NotFound();
    }

}
