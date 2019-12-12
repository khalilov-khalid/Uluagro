using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UluagroAspnet.Models;

namespace UluagroAspnet.Areas.AgroPark.Controllers
{
    public class LoginController : Controller
    {
        OculusEntities db = new OculusEntities();

        // GET: AgroPark/Login
        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult AdminLogin(string email, string password)
        {
            var LoginedWorker = db.Users.Where(x => x.Username == email && x.Password == password).FirstOrDefault();
            
            if (LoginedWorker != null)
            {
                Session["HaveLogin"] = LoginedWorker;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }            
        }

        public ActionResult Logout()
        {
            Session["HaveLogin"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}