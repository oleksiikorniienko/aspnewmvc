using System.Web.Mvc;
using aspnet2.Models;
using aspnet2.Services;
using System.Linq;

namespace aspnet2.Controllers
{
    public class RegisterController : Controller
    {
        ApplicationContext db = new ApplicationContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            if(Session["UserId"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Register(RegisterView model, string returnUrl)
        {
            if(Session["UserId"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            var emailExist = (from user in db.Users
                              where user.Email == model.Email.ToLower()
                              select user).FirstOrDefault();

            if (emailExist != null)
            {
                ModelState.AddModelError("Email", "Email address already exists. Please enter a different email address.");
            }
            else if (ModelState.IsValid)
            {
                User user = new User();
                user.Email = model.Email.ToLower();
                user.UserTypeId = UserType.USER;
                user.Password = PasswordService.Hash(model.Password);

                db.Users.Add(user);
                db.SaveChanges();

                Session["UserId"] = user.Id;
                Session["UserTypeId"] = UserType.USER;

                return RedirectToAction("Index", "Home");
            }

            return View("Index", model);
        }
    }
}
