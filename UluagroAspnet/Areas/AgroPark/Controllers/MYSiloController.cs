using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UluagroAspnet.Models;
using UluagroAspnet.Areas.AgroPark.ModelViews;

namespace UluagroAspnet.Areas.AgroPark.Controllers
{
    [AdminAuthenticationController]
    public class MYSiloController : Controller
    {
        private readonly OculusEntities db = new OculusEntities();
        // GET: AgroPark/MYSİlo
        public ActionResult Index()
        {
            return View(db.MYSILOSTOCKs.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.Silo = db.MYSILOes.ToList();
            return View();
        }

        public ActionResult OperationLoad(int op_id, int silo_id)
        {
            MYSILOSTOCK stock = db.MYSILOSTOCKs.FirstOrDefault(s => s.MYSILOID == silo_id);

            if(op_id == 1)
            {
                if(stock == null)
                {
                    SiloDetailVM vm = new SiloDetailVM
                    {
                        crops = db.CROPs.Where(c => c.PARCEL_CATEGORY_ID == 1).ToList(),
                        repr = db.CROP_REPRODUCTION.ToList()
                    };

                    return PartialView("~/Areas/AgroPark/Views/Shared/_PartialSiloİncomeEmpty.cshtml", vm);
                }
                else
                {
                    if(stock.TOTALCOUNT == 0)
                    {
                        SiloDetailVM vm = new SiloDetailVM
                        {
                            crops = db.CROPs.Where(c => c.PARCEL_CATEGORY_ID == 1).ToList(),
                            sorts = db.CROP_SORT.Where(s => s.CROP_ID == stock.CROP_SORT.CROP_ID).ToList(),
                            repr = db.CROP_REPRODUCTION.ToList(),
                            stock = stock
                        };

                        return PartialView("~/Areas/AgroPark/Views/Shared/_PartialSiloİncomeFullButCountNull.cshtml", vm);
                    }
                    else
                    {
                        return PartialView("~/Areas/AgroPark/Views/Shared/_PartialSiloİncomeFull.cshtml", stock);
                    }
                }
            }
            else
            {
                if(stock != null && stock.TOTALCOUNT != 0)
                {
                    return PartialView("~/Areas/AgroPark/Views/Shared/_PartialSiloOutcome.cshtml", stock);
                }
                else
                {
                    return Content("Siloda ehtiyyat yoxdur!");
                }
            }
        }

        public int AddToSilo(int silo_id, int opr_id, int sort_id, int repr_id, decimal count)
        {
            MYSILOSTOCK stock = db.MYSILOSTOCKs.FirstOrDefault(s => s.MYSILOID == silo_id);

            db.MYSILOSTOCKDETAILS.Add(new MYSILOSTOCKDETAIL
            {
                OPERATIONTYPE = opr_id,
                CROPSORTID = sort_id,
                CROPREPREDUCTIONID = repr_id,
                COUNT = count,
                MYSILOID = silo_id
            });
            db.SaveChanges();

            if (stock == null)
            {
                db.MYSILOSTOCKs.Add(new MYSILOSTOCK
                {
                    MYSILOID = silo_id,
                    CROPSORTID = sort_id,
                    CROPREPREDUCTIONID = repr_id,
                    TOTALCOUNT = count
                });

                db.SaveChanges();
                return 1;
            }
            else if (stock.TOTALCOUNT == 0)
            {
                stock.CROPSORTID = sort_id;
                stock.CROPREPREDUCTIONID = repr_id;
                stock.TOTALCOUNT += count;

                db.SaveChanges();
                return 2;
            }
            else
            {
                stock.TOTALCOUNT += count;
                db.SaveChanges();
                return 3;
            }
        }

        public int TakeFromSilo(int silo_id, int opr_id, decimal count)
        {
            MYSILOSTOCK stock = db.MYSILOSTOCKs.FirstOrDefault(s => s.MYSILOID == silo_id);

            if(count <= stock.TOTALCOUNT)
            {
                stock.TOTALCOUNT -= count;

                db.MYSILOSTOCKDETAILS.Add(new MYSILOSTOCKDETAIL
                {
                    OPERATIONTYPE = opr_id,
                    CROPSORTID = stock.CROPSORTID,
                    CROPREPREDUCTIONID = stock.CROPREPREDUCTIONID,
                    COUNT = count,
                    MYSILOID = silo_id
                });

                db.SaveChanges();

                return 1;
            }
            else
            {
                return 0;
            }
        }

        public ActionResult Details(int id)
        {
            IEnumerable<MYSILOSTOCKDETAIL> list = db.MYSILOSTOCKDETAILS.Where(d => d.MYSILOID == id).ToList();

            return View(list);
        }
    }
}