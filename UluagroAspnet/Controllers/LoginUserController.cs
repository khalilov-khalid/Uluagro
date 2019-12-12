using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UluagroAspnet.Models;

namespace UluagroAspnet.Controllers
{
    public class LoginUserController : Controller
    {
        OculusEntities db = new OculusEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string username, string password)
        {
            var loginedUser = db.Users.Where(s => s.Username == username && s.Password == password && s.UserAdmin_ID==2).FirstOrDefault();
            if (loginedUser!=null)
            {
                Session["UserLogin"] = loginedUser;
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "LoginUser");
            
        }

        public ActionResult Logout()
        {
            Session["UserLogin"] = null;
            return RedirectToAction("Index", "LoginUser");
        }
    }
}