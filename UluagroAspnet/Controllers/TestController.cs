using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UluagroAspnet.Models;

namespace UluagroAspnet.Controllers
{

    public class Pers
    {
        public string Name;
        public string Surname;
    }
    public class TestController : Controller
    {
        // GET: Test
        OculusEntities db = new OculusEntities();
        public ActionResult Index()
        {
            List<Pers> asd = new List<Pers>()
            {
                  new Pers
                  {
                      Name="Shamil",
                      Surname="Kamilli"
                  },
                  new Pers
                  {
                      Name="Khxaiik",
                      Surname="Xelilov"
                  }
            };

            string data = JsonConvert.SerializeObject(asd);


           List<Pers> PErsons = JsonConvert.DeserializeObject<List<Pers>>(data);

            return View();
        }
    }
}