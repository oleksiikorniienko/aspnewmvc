using System.Web.Mvc;
using aspnet2.Models;
using System.Linq;
using aspnet2.Services;

namespace aspnet2.Controllers
{
    public class LoginController : Controller
    {
        ApplicationContext db = new ApplicationContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Login(LoginView model)
        {
            if (Session["UserId"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            if (ModelState.IsValid)
            {
                var loginedUser = (from user in db.Users
                                   where user.Email == model.Email.ToLower()
                                   select user).FirstOrDefault();

                if (loginedUser == null)
                {
                    ModelState.AddModelError("Email", "Email don`t exists. Please enter a correcct email address.");
                }
                else if (PasswordService.IsCorrect(model.Password, loginedUser.Password))
                {
                    Session["UserId"] = loginedUser.Id;
                    Session["UserTypeId"] = loginedUser.UserTypeId;

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("Password", "Incorrect password!");
            }

            return View("Index", model);
        }
    }
}
