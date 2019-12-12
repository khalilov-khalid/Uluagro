using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UluagroAspnet.Models;

namespace UluagroAspnet.Areas.AgroPark.Controllers
{
    [AdminAuthenticationController]
    public class CropPlanningController : Controller
    {
        private OculusEntities db = new OculusEntities();
        // GET: AgroPark/CropPlanning
        public ActionResult Index()
        {
            return View(db.PLANNINGPLANDETAILS.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.Categories = db.PARCEL_CATEGORY.ToList();
            ViewBag.CropReps = db.CROP_REPRODUCTION.ToList();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(int CROPSORTID, int CROPREPRODUCTIONID, IEnumerable<string> MONTH, IEnumerable<string> WORKCANDO, IEnumerable<string> NEEDED, IEnumerable<decimal> PRICE)
        {
            if(CROPSORTID != 0 && CROPREPRODUCTIONID != 0 && MONTH.Where(i => i == "").Count() == 0 && WORKCANDO.Where(i => i.Trim() == "").Count() == 0 && NEEDED.Where(i => i.Trim() == "").Count() == 0 && PRICE.Where(i => i == 0).Count() == 0)
            {
                PLANNINGPLANDETAIL verplan = db.PLANNINGPLANDETAILS.FirstOrDefault(p => p.CROPSORTID == CROPSORTID && p.CROPREPRODUCTIONID == CROPREPRODUCTIONID);
                if(verplan == null)
                {
                    PLANNINGPLANDETAIL plan = new PLANNINGPLANDETAIL();
                    plan.CROPSORTID = CROPSORTID;
                    plan.CROPREPRODUCTIONID = CROPREPRODUCTIONID;
                    plan.SEASON = 0;

                    PLANNINGPLANDETAIL newplan = null;

                    Task addnewplan = Task.Run(() =>
                    {
                        newplan = db.PLANNINGPLANDETAILS.Add(plan);
                        db.SaveChanges();
                    });

                    await addnewplan;

                    for (var i = 0; i < MONTH.Count(); i++)
                    {
                        db.PLANNINGPLANDETAILSWORKS.Add(new PLANNINGPLANDETAILSWORK
                        {
                            PLANNINGPLANDETAILSID = newplan.OBJECTID,
                            MONTH = MONTH.ToList()[i],
                            WORKCANDO = WORKCANDO.ToList()[i],
                            NEEDED = NEEDED.ToList()[i],
                            PRICE = PRICE.ToList()[i]
                        });

                        db.SaveChanges();
                    }

                    TempData["Created"] = "Yeni plan uğurla yaradıldı";
                    return RedirectToAction("Index");
                }
                else
                {
                    for (var i = 0; i < MONTH.Count(); i++)
                    {
                        db.PLANNINGPLANDETAILSWORKS.Add(new PLANNINGPLANDETAILSWORK
                        {
                            PLANNINGPLANDETAILSID = verplan.OBJECTID,
                            MONTH = MONTH.ToList()[i],
                            WORKCANDO = WORKCANDO.ToList()[i],
                            NEEDED = NEEDED.ToList()[i],
                            PRICE = PRICE.ToList()[i]
                        });

                        db.SaveChanges();
                    }

                    TempData["Updated"] = "Seçdiyiniz xüsusiyyətlərə uyğun plan artıq mövcuddur. Yeni xüsusiyyətlər uğurla əlavə olundu";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.CreateError = "Zəhmət olmasa giriş məlumatlarını düzgün daxil edin!";
                ViewBag.Categories = db.PARCEL_CATEGORY.ToList();
                ViewBag.CropReps = db.CROP_REPRODUCTION.ToList();
                return View();
            }
        }

        public ActionResult LoadCrops(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable<CROP> crops = db.CROPs.Where(c => c.PARCEL_CATEGORY_ID == id);

            return Json(crops,JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadCropSorts(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable<CROP_SORT> cropsorts = db.CROP_SORT.Where(c => c.CROP_ID == id);

            return Json(cropsorts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddPlan()
        {
            return PartialView("~/Areas/AgroPark/Views/Shared/_PartialPlanAdd.cshtml");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            PLANNINGPLANDETAIL slctdplan = db.PLANNINGPLANDETAILS.Find(id);

            if (slctdplan == null)
            {
                return HttpNotFound();
            }

            ViewBag.Months = new List<string> { "Yanvar", "Fevral", "Mart", "Aprel", "May", "Iyun", "Iyul", "Avqust", "Sentyabr", "Oktyabr", "Noyabr", "Dekabr" };
            return View(db.PLANNINGPLANDETAILSWORKS.Where(p => p.PLANNINGPLANDETAILSID == id).ToList());
        }

        [HttpPost]
        public ActionResult Edit(IEnumerable<int> obj_id, IEnumerable<string> MONTH, IEnumerable<string> WORKCANDO, IEnumerable<string> NEEDED, IEnumerable<decimal> PRICE)
        {
            for(var i = 0; i < obj_id.Count(); i++)
            {
                PLANNINGPLANDETAILSWORK plan = db.PLANNINGPLANDETAILSWORKS.Find(obj_id.ToList()[i]);
                plan.MONTH = MONTH.ToList()[i];
                plan.WORKCANDO = WORKCANDO.ToList()[i];
                plan.NEEDED = NEEDED.ToList()[i];
                plan.PRICE = PRICE.ToList()[i];

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}