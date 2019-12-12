using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UluagroAspnet.Models;
using UluagroAspnet.Mainclasses;
using System.Data.Entity;
using UluagroAspnet.ClassAJAXS;
using UluagroAspnet.ClassGetAjaxs;
using System.Xml;
using UluagroAspnet.Mainclasses.NewWorkPlan;
using System.Threading.Tasks;

namespace UluagroAspnet.Controllers
{
    public class AjaxsRequestController : Controller
    {
        OculusEntities db = new OculusEntities();
        // GET: AjaxsRequest

        //=========================================> bildiris ve mesaj bolmesi <================================================

        //sisteme giris yapan adamin id-sine gore umumi mesajlarin getirilmesi
        public ActionResult Notification()
        {
            User logineduser = (User)Session["UserLogin"];
            List<NotificationFormat> mesagelist = new List<NotificationFormat>();
            mesagelist = db.MESSAGENOTIFICATIONs.Where(w => w.RECIVERID == logineduser.ID).OrderByDescending(a => a.OBJECTID).Select(s => new NotificationFormat()
            {
                Objectid = s.OBJECTID,
                SenderName = db.Users.Where(t => t.ID == s.SENDERID).FirstOrDefault().Worker.Name + " " + db.Users.Where(t => t.ID == s.SENDERID).FirstOrDefault().Worker.Surname,
                messagename = db.WORK_PLAN.Where(q => q.OBJECTID == s.WORKPLANID).FirstOrDefault().WORK.NAME,
                BeginTime = (DateTime)db.WORK_PLAN.Where(q => q.OBJECTID == s.WORKPLANID).FirstOrDefault().START_DATE,
                EndTime = (DateTime)db.WORK_PLAN.Where(q => q.OBJECTID == s.WORKPLANID).FirstOrDefault().END_DATE,
                Status = s.STATUS,
                WorkStatus = (bool)db.WORK_PLAN.Where(q => q.OBJECTID == s.WORKPLANID).FirstOrDefault().STATUS,
                MessageSendedTime=s.SENDEDTIME
            }).ToList(); 
            return PartialView("_PartialNotification",mesagelist);
        }

        //sisteme giris eden sexsin oxunmamis mesajlari sayini qaytarir
        public  ActionResult CheckNotificationCount()
        {
            if (Session["UserLogin"] == null)
            {
                return JavaScript("window.location = 'http://demo.uluagro.az'");
            }
            User logineduser = (User)Session["UserLogin"];
            int MessagesCount = db.MESSAGENOTIFICATIONs.Where(w => w.RECIVERID == logineduser.ID && w.STATUS==true).Count();
            return Json(MessagesCount, JsonRequestBehavior.AllowGet);
        }

        //sisteme giris eden sexsin id-sine gonderilen mesajdaki is planlamasinin bitmesini yerine yetiren emeliyyat. is planlamasinin statusunu deyisir
        public ActionResult WorkPlanComplated(int id)
        {
            MESSAGENOTIFICATION compilitedWp = db.MESSAGENOTIFICATIONs.Find(id);

            WORK_PLAN complatedWorkPlan = db.WORK_PLAN.Find(compilitedWp.WORKPLANID);
            complatedWorkPlan.STATUS = false;
            db.Entry(complatedWorkPlan).State = EntityState.Modified;
            db.SaveChanges();
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        //sisteme giris edien sexsin id-sine gore gelen mesagin oxuduqdan sonra statusunu deyisdiren emeliyyat.
        public ActionResult Reading(int id)
        {
            MESSAGENOTIFICATION reading = db.MESSAGENOTIFICATIONs.Find(id);
            reading.STATUS = false;
            db.Entry(reading).State = EntityState.Modified;
            db.SaveChanges();
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        //=====================================================> bildiris ve mesaj sistemlerinin kodlarinin bitdiyi yer <========================================================

        

        //=====================================================new Work Plan=====================================================
        //isplanlamasin esas hissesi 
        public ActionResult GetAllDataWichWorkPlan()
        {
            MainWorkPlanVM WorkPlanMainData = new MainWorkPlanVM();
            WorkPlanMainData.Responders = db.Workers.Where(w => w.Profession.CanResponser == true).ToList();
            WorkPlanMainData.WorkCategories = db.WORKS_CATEGORY.ToList();
            WorkPlanMainData.ParcelCategories = db.PARCEL_CATEGORY.ToList();
            return PartialView("~/Views/RazorPages/WorkPlan/_CreateAndUpdateWorkPlan.cshtml", WorkPlanMainData);
        }

        //is planlamasinda esas hissede is categoriyasinda gorulecek islerin siyahisini qaytarir
        public ActionResult WorkCategoryChange(int id)
        {
            List<WORK> worklist = new List<WORK>();
            db.Configuration.ProxyCreationEnabled = false;
            worklist = db.WORKS.Where(a => a.WORK_CAT_ID == id).ToList();
            return Json(worklist, JsonRequestBehavior.AllowGet);
        }

        //gelen sahe categoriyasinin id-sine gore saheni qaytarir 
        public ActionResult ParselCategoryChange(int id)
        {
            List<ParcelArray> parcelList = new List<ParcelArray>();
            db.Configuration.ProxyCreationEnabled = false;
            parcelList = db.PARCELs.Where(s => s.PARCEL_CATEGORY_ID == id).Select(a=>new ParcelArray() {
                OBJECTID=a.OBJECTID,
                NAME=a.NAME,
                PARCELCATEGORYID=(int)a.PARCEL_CATEGORY_ID,
                AREA=(decimal)a.AREA
            }).ToList();
            return Json(parcelList, JsonRequestBehavior.AllowGet);
        }


        public  ActionResult WorkPlanAddedNewTask()
        {

            return PartialView("~/Views/RazorPages/WorkPlan/_WorkPlanTask.cshtml");
        }

        //Tapsiriqlarin icerisinde nece novbe olacagini teyin edir.
        public ActionResult WorkPlanTaskQueue(int queue)
        {
            QueueWorkPlanTask TaskQueue = new QueueWorkPlanTask();
            TaskQueue.QueueCount = queue;
            TaskQueue.WorkerProfessions = db.Professions.ToList();
            return PartialView("~/Views/RazorPages/WorkPlan/_WorkPlanTaskQueue.cshtml", TaskQueue);
        }

        //derman ve gubreler bilmesine yeni derman va ya gubre elave etmek ucun line edlave edir,
        public ActionResult WorkPlanAddedFeltilizerLine()
        {
            List<CATEGORy> Feltilizercategories = db.CATEGORIES.ToList();
            return PartialView("~/Views/RazorPages/WorkPlan/_WorkPlanFeltilizers.cshtml", Feltilizercategories);
        }

        //derman ve ya gubre olmagini onun kategoriyasina gore teyin edir ve adlarini getirir.
        public ActionResult WorkPlanFeltilizerByCategory(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<FERTILIZER> feltilizerlist = db.FERTILIZERs.Where(s => s.CATEGORY_ID == id).ToList();
            return Json(feltilizerlist, JsonRequestBehavior.AllowGet);
        }

        //tasklarin icerisinde heovbelere texnikalar elave edir.
        public ActionResult WorkPlanAddedTaskNewTechnicalLine()
        {
            TechniqueLIneWorkPlanVM tex = new TechniqueLIneWorkPlanVM();
            tex.texCategory = db.TECHNIQUE_TYPE.ToList();
            tex.drivers = db.Workers.Where(s => s.Professions_id == 8).ToList();
            return PartialView("~/Views/RazorPages/WorkPlan/_WorkPlanTechnical.cshtml", tex);
        }

        //tasklarin icerisinde novbelerin ozlerinde texnika categoriyasina gore texnikalarin listini gonderir.
        public ActionResult WorkPlanTaskQueueTecnical(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<TECHNIQUE> techlist = db.TECHNIQUES.Where(s => s.TYPE_ID == id).ToList();
            return Json(techlist, JsonRequestBehavior.AllowGet);
        }


        //  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< WORK PLAN>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        // get all data work plan
        public ActionResult TechnicalCategoryList()
        {
            WorkPlanViewModel vm = new WorkPlanViewModel();
            db.Configuration.ProxyCreationEnabled = false;
            vm._TechniqueCategory= db.TECHNIQUE_TYPE.ToList();
            vm._Technique = db.TECHNIQUES.Select(s => new TechnicalArray
            {
                OBJECTID = s.OBJECTID,
                CATEGORYID = (int)s.TYPE_ID,
                NAME = s.NAME,
                CONDITIONSTATUS = (int)s.CONDITION_ID,
                WORKINGSTATUS = (int)s.WORKING_TYPE_ID
            }).ToList();
            vm._FeltilizerCAtegory = db.CATEGORIES.ToList();
            vm._Feltilizer = db.FERTILIZERs.Select(s => new FeltilizerArray
            {
                OBJECTID = s.OBJECTID,
                CATEGORYID = (int)s.CATEGORY_ID,
                NAME = s.NAME,
                WATERKG = (decimal)s.WATER_PER_KG
            }).ToList();
            vm._ParcelCategory = db.PARCEL_CATEGORY.ToList();
            vm._Parcel = db.PARCELs.Where(s => s.PARCEL_CATEGORY_ID != null).Select(s => new ParcelArray
            {
                OBJECTID = s.OBJECTID,
                PARCELCATEGORYID = (int)s.PARCEL_CATEGORY_ID,
                NAME = s.NAME,
                AREA = (decimal)s.AREA
            }).ToList();
            vm._Responders = db.Workers.Where(s=>s.Profession.CanResponser==true).ToList();
            vm._WorkCategory = db.WORKS_CATEGORY.Select(s=>new WorkCategoryArray
            {
                OBJECTID=s.OBJECTID,
                NAME=s.NAME
            }).ToList();
            vm._Work = db.WORKS.ToList();
            vm._Profession = db.Professions.Select(s=> new ProfessionArray
            {
                OBJECTID=s.ID,
                NAME=s.Name
            }).ToList();
            vm._Worker = db.Workers.Select(s => new WorkerArray
            {
                OBJECTID = s.ID,
                FULLNAME = s.Name + " " + s.Surname,
                PROFESSIONID = s.Professions_id,
                WorkingStatus = 1
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);

        }

        
        //create work plan
        public ActionResult CreateWorkPlan(mainWorkPlan workplan)
        {
            User logineduser = (User)Session["UserLogin"];

            WORK_PLAN newworkplan = new WORK_PLAN();
            newworkplan.START_DATE = workplan.mainstartdate;
            newworkplan.END_DATE = workplan.mainenddate;
            newworkplan.PARCEL_ID = workplan.parcelId;
            newworkplan.RESPONDER_ID = workplan.responderTd;
            newworkplan.WORKS_ID = workplan.WorkId;
            newworkplan.STATUS = true;
            db.WORK_PLAN.Add(newworkplan);
            db.SaveChanges();


            User reciver = db.Users.Where(s => s.WorkerID == newworkplan.RESPONDER_ID).FirstOrDefault();

            if (reciver!=null)
            {

                MESSAGENOTIFICATION new_message = new MESSAGENOTIFICATION();
                new_message.WORKPLANID = newworkplan.OBJECTID;
                new_message.STATUS = true;
                new_message.RECIVERID = (int)reciver.ID;
                new_message.SENDERID = logineduser.ID;
                new_message.SENDEDTIME = DateTime.Now;
                db.MESSAGENOTIFICATIONs.Add(new_message);
                db.SaveChanges();
            }

            


            if (workplan.dailyWorkPlanArray!=null && workplan.dailyWorkPlanArray.Count!=0)
            {
                for (int i = 0; i < workplan.dailyWorkPlanArray.Count; i++)
                {
                    DAYLY_WORK_PLAN newdailyPlan = new DAYLY_WORK_PLAN();
                    newdailyPlan.WORKPLAN_ID = newworkplan.OBJECTID;
                    newdailyPlan.STARTDATE = workplan.dailyWorkPlanArray[i].startdate;
                    newdailyPlan.ENDDATE = workplan.dailyWorkPlanArray[i].enddate;
                    db.DAYLY_WORK_PLAN.Add(newdailyPlan);
                    db.SaveChanges();

                    if (workplan.dailyWorkPlanArray[i].queueArray != null && workplan.dailyWorkPlanArray[i].queueArray.Count!=0)
                    {
                        for (int q = 0; q < workplan.dailyWorkPlanArray[i].queueArray.Count; q++)
                        {
                            QueuePlan newqueueplan = new QueuePlan();
                            newqueueplan.dailyPlanId = newdailyPlan.OBJECTID;
                            newqueueplan.startdate = DateTime.Now;
                            newqueueplan.enddate = DateTime.Now;
                            newqueueplan.note = workplan.dailyWorkPlanArray[i].queueArray[q].note;
                            db.QueuePlans.Add(newqueueplan);
                            db.SaveChanges();

                            if (workplan.dailyWorkPlanArray[i].queueArray[q].workerID!=null && workplan.dailyWorkPlanArray[i].queueArray[q].workerID.Count!=0)
                            {
                                for (int w = 0; w < workplan.dailyWorkPlanArray[i].queueArray[q].workerID.Count; w++)
                                {
                                    WORK_PLAN_WORKERS planworkers = new WORK_PLAN_WORKERS();
                                    planworkers.DAYLY_WORK_PLAN_ID = newqueueplan.OBJECTID;
                                    planworkers.WORKER_ID = workplan.dailyWorkPlanArray[i].queueArray[q].workerID[w];
                                    db.WORK_PLAN_WORKERS.Add(planworkers);
                                    db.SaveChanges();
                                }
                            }

                            if (workplan.dailyWorkPlanArray[i].queueArray[q].technialArray!=null && workplan.dailyWorkPlanArray[i].queueArray[q].technialArray.Count != 0)
                            {
                                for (int t = 0; t < workplan.dailyWorkPlanArray[i].queueArray[q].technialArray.Count; t++)
                                {
                                    WORK_PLAN_TECHNIQUE plantechicals = new WORK_PLAN_TECHNIQUE();
                                    plantechicals.DAYLY_WORK_PLAN_ID = newqueueplan.OBJECTID;
                                    plantechicals.TECHNIQUE_ID = workplan.dailyWorkPlanArray[i].queueArray[q].technialArray[t].tehcnical;
                                    db.WORK_PLAN_TECHNIQUE.Add(plantechicals);
                                    db.SaveChanges();

                                    if (workplan.dailyWorkPlanArray[i].queueArray[q].technialArray[t].trailerid!=null && workplan.dailyWorkPlanArray[i].queueArray[q].technialArray[t].trailerid.Count!=0)
                                    {
                                        for (int y = 0; y < workplan.dailyWorkPlanArray[i].queueArray[q].technialArray[t].trailerid.Count; y++)
                                        {
                                            TECHNIQUE_TRAILER plantrailer = new TECHNIQUE_TRAILER();
                                            plantrailer.WORK_PLAN_TECHNIQUE_ID = plantechicals.OBJECTID;
                                            plantrailer.TECHNIQUE_ID = workplan.dailyWorkPlanArray[i].queueArray[q].technialArray[t].trailerid[y];
                                            db.TECHNIQUE_TRAILER.Add(plantrailer);
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            }

                            if (workplan.dailyWorkPlanArray[i].queueArray[q].feltilizerArray != null && workplan.dailyWorkPlanArray[i].queueArray[q].feltilizerArray.Count!=0)
                            {
                                for (int f = 0; f < workplan.dailyWorkPlanArray[i].queueArray[q].feltilizerArray.Count; f++)
                                {
                                    FERTILIZER_WORK_PLAN planfertilizer = new FERTILIZER_WORK_PLAN();
                                    planfertilizer.DAYLY_WORK_PLAN_ID = newqueueplan.OBJECTID;
                                    planfertilizer.FERTILIZER_ID = workplan.dailyWorkPlanArray[i].queueArray[q].feltilizerArray[f].feltilizerid;
                                    planfertilizer.WATER_UNIT = Convert.ToInt32(workplan.dailyWorkPlanArray[i].queueArray[q].feltilizerArray[f].watherquantity);
                                    planfertilizer.FERTILIZER_UNIT= Convert.ToInt32(workplan.dailyWorkPlanArray[i].queueArray[q].feltilizerArray[f].feltilizerquantity);
                                    db.FERTILIZER_WORK_PLAN.Add(planfertilizer);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }    
                }
            }       

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<END WORK PLAN>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>



        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< FINANCIAL RAPORT>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        public ActionResult FinancialTypeInout()
        {
            FinancialTypeInounViewModel vm = new FinancialTypeInounViewModel();
            db.Configuration.ProxyCreationEnabled = false;
            vm._PaymentInout = db.PAYMENT_INOUT.ToList();
            vm._PaymentType = db.PAYMENT_TYPE.ToList();
            vm._AllFinancialReport = db.PAYMENTs.OrderByDescending(s=>s.OBJECTID).Select(s=>new CopyPayment
            {
                OBJECTID=s.OBJECTID,
                DATE=(DateTime)s.DATE,
                ID_INOUT=s.PAYMENT_INOUT.NAME,
                PAYMENT_TYPE_ID=s.PAYMENT_TYPE.NAME,
                MAIN_PAYMENT=(decimal)s.MAIN_PAYMENT,
                VAT=(decimal)s.VAT,
                ALL_PAYMENT=(decimal)s.ALL_PAYMENT,
                RELEASE=s.RELEASE,
                REMNANT=(decimal)s.REMNANT,
                NOTE=s.NOTE                
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CreateFinancialRaport(PAYMENT _payment)
        {
            bool checkPayment=true;
            decimal currentCash = (decimal)db.CURRENT_ACCOUNT.First().CURRENT_BANK_CASH;
            decimal currentHandCash = (decimal)db.CURRENT_ACCOUNT.First().HAND_CASH;
            if (_payment.ID_INOUT == 2 && _payment.PAYMENT_TYPE_ID == 1)
            {
                if (currentHandCash >= _payment.MAIN_PAYMENT)
                {
                    checkPayment = true;
                }
                else
                {
                    checkPayment = false;
                }
            }
            else if (_payment.ID_INOUT == 2 && _payment.PAYMENT_TYPE_ID != 1)
            {
                if (currentCash >= _payment.MAIN_PAYMENT)
                {
                    checkPayment = true;
                }
                else
                {
                    checkPayment = false;
                }
            }


            if (checkPayment==true)
            {
                db.PAYMENTs.Add(_payment);
                db.SaveChanges();
                if (_payment.ID_INOUT == 1)
                {
                    if (_payment.PAYMENT_TYPE_ID == 1)
                    {
                        var current = db.CURRENT_ACCOUNT.FirstOrDefault();
                        current.HAND_CASH += _payment.MAIN_PAYMENT;
                        current.MAIN_CASH += _payment.MAIN_PAYMENT;
                        current.VAT += _payment.VAT;
                        db.SaveChanges();

                        var existpay = db.PAYMENTs.Find(_payment.OBJECTID);
                        existpay.REMNANT = current.MAIN_CASH;
                        db.SaveChanges();
                    }
                    else
                    {
                        var current = db.CURRENT_ACCOUNT.FirstOrDefault();
                        current.CURRENT_BANK_CASH += _payment.MAIN_PAYMENT;
                        current.MAIN_CASH += _payment.MAIN_PAYMENT;
                        current.VAT += _payment.VAT;
                        db.SaveChanges();

                        var existpay = db.PAYMENTs.Find(_payment.OBJECTID);
                        existpay.REMNANT = current.MAIN_CASH;
                        db.SaveChanges();
                    }
                }
                if (_payment.ID_INOUT == 2)
                {
                    if (_payment.PAYMENT_TYPE_ID == 1)
                    {
                        var current = db.CURRENT_ACCOUNT.FirstOrDefault();
                        current.HAND_CASH -= _payment.MAIN_PAYMENT;
                        current.MAIN_CASH -= _payment.MAIN_PAYMENT;
                        current.VAT -= _payment.VAT;
                        db.SaveChanges();

                        var existpay = db.PAYMENTs.Find(_payment.OBJECTID);
                        existpay.REMNANT = current.MAIN_CASH;
                        db.SaveChanges();
                    }
                    else
                    {
                        var current = db.CURRENT_ACCOUNT.FirstOrDefault();
                        current.CURRENT_BANK_CASH -= _payment.MAIN_PAYMENT;
                        current.MAIN_CASH -= _payment.MAIN_PAYMENT;
                        current.VAT -= _payment.VAT;
                        db.SaveChanges();

                        var existpay = db.PAYMENTs.Find(_payment.OBJECTID);
                        existpay.REMNANT = current.MAIN_CASH;
                        db.SaveChanges();
                    }
                }

                db.Configuration.ProxyCreationEnabled = false;
                List<CopyPayment> PaymentList = db.PAYMENTs.OrderByDescending(s => s.OBJECTID).Select(s => new CopyPayment
                {
                    OBJECTID = s.OBJECTID,
                    DATE = (DateTime)s.DATE,
                    ID_INOUT = s.PAYMENT_INOUT.NAME,
                    PAYMENT_TYPE_ID = s.PAYMENT_TYPE.NAME,
                    MAIN_PAYMENT = (decimal)s.MAIN_PAYMENT,
                    VAT = (decimal)s.VAT,
                    ALL_PAYMENT = (decimal)s.ALL_PAYMENT,
                    RELEASE = s.RELEASE,
                    REMNANT = (decimal)s.REMNANT,
                    NOTE = s.NOTE
                }).ToList();

                return Json(PaymentList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult UpdateReport(int _payment)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var IpdateReport = db.PAYMENTs.Find(_payment);
            return Json(IpdateReport, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FinishUpdateReport(PAYMENT _payment, int _id)
        {
            var report = db.PAYMENTs.Find(_id);
            report.DATE = _payment.DATE;
            report.ID_INOUT = _payment.ID_INOUT;
            report.PAYMENT_TYPE_ID = _payment.PAYMENT_TYPE_ID;
            report.MAIN_PAYMENT = _payment.MAIN_PAYMENT;
            report.VAT = _payment.VAT;
            report.ALL_PAYMENT = _payment.ALL_PAYMENT;
            report.RELEASE = _payment.RELEASE;
            report.REMNANT = 0;
            report.NOTE = _payment.NOTE;
            db.SaveChanges();

            List<PAYMENT> allPayments = db.PAYMENTs.ToList();
            for (int i = 0; i < allPayments.Count; i++)
            {
                if (allPayments[i].OBJECTID >= _id)
                {                    
                    if (allPayments[i].ID_INOUT==1)
                    {
                        allPayments[i].REMNANT = (allPayments[i - 1].REMNANT + allPayments[i].MAIN_PAYMENT);
                        db.SaveChanges();
                    }
                    if (allPayments[i].ID_INOUT == 2)
                    {
                        allPayments[i].REMNANT = (allPayments[i - 1].REMNANT - allPayments[i].MAIN_PAYMENT);
                        db.SaveChanges();
                    }
                }
            }

            db.Configuration.ProxyCreationEnabled = false;
            List<CopyPayment> PaymentList = db.PAYMENTs.OrderByDescending(s => s.OBJECTID).Select(s => new CopyPayment
            {
                OBJECTID = s.OBJECTID,
                DATE = (DateTime)s.DATE,
                ID_INOUT = s.PAYMENT_INOUT.NAME,
                PAYMENT_TYPE_ID = s.PAYMENT_TYPE.NAME,
                MAIN_PAYMENT = (decimal)s.MAIN_PAYMENT,
                VAT = (decimal)s.VAT,
                ALL_PAYMENT = (decimal)s.ALL_PAYMENT,
                RELEASE = s.RELEASE,
                REMNANT = (decimal)s.REMNANT,
                NOTE = s.NOTE
            }).ToList();

            return Json(PaymentList, JsonRequestBehavior.AllowGet);
        }
        

        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< END FINANCIAL RAPORT>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


        public ActionResult Depos()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var _depos = db.STOCKPLACES.ToList();
            return Json(_depos, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Depodetails(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var allStock = db.STOCKMODIFies.Where(w => w.PLACEID == id).GroupBy(s => s.STOCKITEMID).ToList();

            List<ShowDepoStockView> vmList = new List<ShowDepoStockView>();

            foreach (var item in allStock)
            {
                ShowDepoStockView vm = new ShowDepoStockView();
                int x = (int)item.FirstOrDefault().STOCKITEMID;
                var _stock = db.STOCKS.Where(s => s.OBJECTID == x).FirstOrDefault();
                if (_stock.TYPE == 1)
                {
                    vm.NAME = db.FERTILIZERs.Where(s => s.OBJECTID == _stock.PRODUCT_ID).FirstOrDefault().NAME;
                    vm.TYPE = "Dərman";
                }
                if (_stock.TYPE == 2)
                {
                    vm.NAME = db.FERTILIZERs.Where(s => s.OBJECTID == _stock.PRODUCT_ID).FirstOrDefault().NAME;
                    vm.TYPE = "Gübrə";
                }
                if (_stock.TYPE == 3)
                {
                    vm.NAME = db.TECHNIQUES.Where(s => s.OBJECTID == _stock.PRODUCT_ID).FirstOrDefault().NAME;
                }
                decimal total = (decimal)item.Where(s => s.STATUSID == 1).Sum(s => s.QUANTITY);
                decimal minusdata = (decimal)item.Where(s => s.STATUSID == 2).Sum(t => t.QUANTITY);

                vm.QUANTITY = (total - minusdata).ToString();

                vmList.Add(vm);

            }

            return Json(vmList, JsonRequestBehavior.AllowGet);
        }


        //======================================================Crop Planning Actions======================================================

        public ActionResult LoadParcelCategories()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.PARCEL_CATEGORY.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadCrops(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.CROPs.Where(c => c.PARCEL_CATEGORY_ID == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadParcels(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<ParcelArray> parcellist = db.PARCELs.Where(p => p.PARCEL_CATEGORY_ID == id).Select(s => new ParcelArray()
            {
                OBJECTID = s.OBJECTID,
                PARCELCATEGORYID=(int)s.PARCEL_CATEGORY_ID,
                NAME=s.NAME,
                AREA=(decimal)s.AREA
            }).ToList();
            return Json(parcellist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadCropSorts(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            
            return Json(db.PLANNINGPLANDETAILS.Where(i => i.CROP_SORT.CROP_ID == id).Select(a => new { a.CROP_SORT.OBJECTID, a.CROP_SORT.NAME }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadReproduction(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.PLANNINGPLANDETAILS.Where(i => i.CROP_SORT.CROP_ID == id).Select(a => new { a.CROP_REPRODUCTION.OBJECTID, a.CROP_REPRODUCTION.NAME }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadPlanTable(int rep_id, int sort_id, int parcel_id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var list = db.PLANNINGPLANDETAILSWORKS.Where(p => p.PLANNINGPLANDETAIL.CROPREPRODUCTIONID == rep_id && p.PLANNINGPLANDETAIL.CROPSORTID == sort_id).ToList();

            

            if(list.Count == 0)
            {
                return PartialView("~/Views/Shared/_PartialNoData.cshtml");
            }
            else
            {
                PlanDataVM vm = new PlanDataVM
                {
                    planlist = list,
                    area = (decimal)db.PARCELs.Where(p => p.OBJECTID == parcel_id).FirstOrDefault().AREA,
                    summary = list.Sum(p => p.PRICE)
                };

                return PartialView("_PartialPlanData", vm);
            }

        }

        public async Task<string> SaveAllPlanData(DateTime plan_date, int parcel_id, int sort_id, int repr_id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var list = db.PLANNINGPLANDETAILSWORKS.Where(p => p.PLANNINGPLANDETAIL.CROPREPRODUCTIONID == repr_id && p.PLANNINGPLANDETAIL.CROPSORTID == sort_id).ToList();
            var area = (decimal)db.PARCELs.Where(p => p.OBJECTID == parcel_id).FirstOrDefault().AREA;

            if(list.Count() != 0)
            {
                PLANNINGPLAN plan = new PLANNINGPLAN();
                plan.PLANNINGDATE = plan_date;
                plan.PARCELID = parcel_id;
                plan.CROPSORTID = sort_id;
                plan.CROPREPRODUCTIONID = repr_id;
                plan.CREATEDDATE = DateTime.Now;
                plan.SEASON = 0;

                PLANNINGPLAN newplan = null;

                Task addnewplan = Task.Run(() =>
                {
                    newplan = db.PLANNINGPLANs.Add(plan);
                    db.SaveChanges();
                });

                await addnewplan;

                foreach (var item in list)
                {
                    db.PLANINGPLANBYDETAILS.Add(new PLANINGPLANBYDETAIL
                    {
                        PLANNINGPLANID = newplan.OBJECTID,
                        PLANNINGDETAISID = item.PLANNINGPLANDETAILSID,
                        PLANNINGDETAISWORKID = item.OBJECTID,
                        FINALLYPRICE = item.PRICE * area
                    });

                    db.SaveChanges();
                }

                return "OK";
            }
            else
            {
                return "FAIL";
            }
        }

        public ActionResult LoadPlaningPlans()
        {
            var data = db.PLANNINGPLANs.ToList();

            return PartialView("_PartialLoadPlanningPlans",  data);
        }

        public ActionResult ShowPlanDetail(int id)
        {
            PlanDetailVM vm = new PlanDetailVM()
            {
                selectedPlan = db.PLANNINGPLANs.FirstOrDefault(p => p.OBJECTID == id),
                detailsList = db.PLANINGPLANBYDETAILS.Where(p => p.PLANNINGPLANID == id)
            };

            return PartialView("_PartialPlanDetails",vm);
        }

        public ActionResult ShowMYSiloDetail(int id)
        {
            MYSILOSTOCK data = db.MYSILOSTOCKs.FirstOrDefault(s => s.MYSILOID == id);

            return PartialView("_PartialSiloDetail", data);
        }
    }
}