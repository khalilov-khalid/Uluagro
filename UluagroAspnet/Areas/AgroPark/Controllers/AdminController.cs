using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UluagroAspnet.Models;

namespace UluagroAspnet.Areas.AgroPark.Controllers
{
    [AdminAuthenticationController]
    public class AdminController : Controller
    {
        OculusEntities db = new OculusEntities();
        // GET: AgroPark/Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangePassword(string newpassword)
        {

           
            User loguser = (User)Session["HaveLogin"];            
            if (loguser != null)
            {
                var user = db.Users.Find(loguser.ID);
                user.Password = newpassword;
                db.SaveChanges();
            }
            var jsonResult = Json(new { data = "Şifrə ugurla yeniləndi" }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }

    
}