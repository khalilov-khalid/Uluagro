using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UluagroAspnet.Models;
using System.Data.Entity;
using UluagroAspnet.Mainclasses;
using System.Data.SqlClient;
using UluagroAspnet.ClassGetAjaxs;

namespace UluagroAspnet.Controllers
{
    [UserAuthenticationController]
    public class HomeController : Controller
    {
        OculusEntities db = new OculusEntities();
        public ActionResult Index()
        {
            User LoginedUSer= (User)Session["UserLogin"];
            MainViewModel vm = new MainViewModel();
            vm._workers = db.Workers.ToList();
            vm._ehtiyyat = db.FERTILIZER_STOCK.ToList();
            vm._loginedUser = LoginedUSer;
            vm._workPlanList = db.WORK_PLAN.OrderByDescending(s=>s.OBJECTID).ToList();
            vm.docmsgcount = db.DOCMESSAGEs.Where(m => m.RECIVED == LoginedUSer.WorkerID && m.OPRTYPE == 3).Count();
            vm._CurrentAccount = db.CURRENT_ACCOUNT.First();
            //vm._Pvots = db.PVOT_S.Distinct().ToList();
            ViewBag.Silo = db.MYSILOes.ToList();
            ViewBag.Professions = db.Professions.Where(p => p.CanResponser == true).ToList();
            ViewBag.ProfessionsAll = db.Professions.ToList();
            return View(vm);
        }       
    }
}