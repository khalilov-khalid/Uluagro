using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UluagroAspnet.Models;
using UluagroAspnet.Mainclasses;

namespace UluagroAspnet.Areas.AgroPark.Controllers
{
    public class PartialController : Controller
    {
        // GET: AgroPark/Partial
        public PartialViewResult Index()
        {
            User logieduser = (User)Session["HaveLogin"];            
            return PartialView(logieduser);
        }
    }
}