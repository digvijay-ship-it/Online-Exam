using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.DataAccess.data;
using OnlineExam.DataAccess.Repository.IRepository;
using OnlineExam.Models.ViewModels;
using OnlineExam.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Online_Exam_Web.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;
        public AccountController(IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }

        //get
        public IActionResult Registeration()
        {
            var a = new RegistrationViewModel()
            {
                SubjectList = _unitOfWork.SubRepo.GetAll().Select(a => new SelectListItem
                {
                    Text = a.Subject_Name,
                    Value = a.Id.ToString()
                }).ToList<SelectListItem>(),
            };
            return View(a);
        }

        [HttpPost]
        public IActionResult Registeration(RegistrationViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                user.Email = obj.Email;
                user.Name = obj.Name;
                user.Password = obj.Password;
                _unitOfWork.UserRepo.Add(user);
                _unitOfWork.Save();
                var a = _unitOfWork.UserRepo.GetFirstOrDefault(e => e.Email == obj.Email);

                IList<UserSubject> UserSubs = new List<UserSubject>();
                var subids = _unitOfWork.SubRepo.GetAll();
                foreach (var sub in subids)
                {
                    var ag = new UserSubject();
                    ag.UserId = a.Id;
                    ag.SubjectId = sub.Id;
                    UserSubs.Add(ag);
                }

                //add range in usersubjects vie addrange
                _unitOfWork.UserSub.AddRange((IList<UserSubject>)UserSubs);
                _unitOfWork.Save();

                TempData["success"] = "you are Eligibal To login with these Credential";
                return RedirectToAction("Login");
            }
            return View(obj);
        }



        [AcceptVerbs("Post", "Get")]
        public IActionResult EmailIsExist(string Email)
        {
            var data = _unitOfWork.UserRepo.GetFirstOrDefault(e => e.Email == Email);
            if (data != null)
            {
                return Json($"Email{Email} already in Use");
            }
            else
            {
                return Json(true);
            }
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Employee or Admin Login
                var Userdata = _unitOfWork.UserRepo.GetFirstOrDefault(e => e.Email == model.Email);
                var AdminData = _unitOfWork.AdminRepo.GetFirstOrDefault(e => e.Email == model.Email);
                if (Userdata != null)
                {
                    bool isValid = (Userdata.Email == model.Email && Userdata.Password == model.Password && Userdata.IsActive);
                    if (isValid)
                    {
                        var identity = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier,Userdata.Id.ToString()),
                            new Claim(ClaimTypes.Email, model.Email),
                            new Claim(ClaimTypes.Name, Userdata.Name),
                            new Claim(ClaimTypes.Role,Userdata.RoleId.ToString())
                        },
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("Username", Userdata.Name);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (!(Userdata.IsActive))
                    {
                        TempData["errorMessageP"] = "User is deactivated by admin try, contacting admin from Mail";
                        return View(model);
                    }
                    else
                    {
                        TempData["errorMessageP"] = "Invalid password!";
                        return View(model);
                    }
                }
                else if (AdminData != null)
                {
                    bool isValidM = (AdminData.Email == model.Email && AdminData.Password == model.Password);
                    if (isValidM)
                    {
                        var identity = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier,AdminData.Id.ToString()),
                            new Claim(ClaimTypes.Name, AdminData.Email),
                            new Claim(ClaimTypes.Email, model.Email),
                            new Claim(ClaimTypes.Role,AdminData.RoleId.ToString())
                        },
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("Username", AdminData.Email);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["errorMessageP"] = "Invalid password!";
                        return View(model);
                    }
                }
                else
                {
                    TempData["errorMessageU"] = "Username not found!";
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedCookies = Request.Cookies.Keys;
            foreach (var cookies in storedCookies)
            {
                Response.Cookies.Delete(cookies);
            }
            return RedirectToAction("Login", "Account");
        }

    }
}
