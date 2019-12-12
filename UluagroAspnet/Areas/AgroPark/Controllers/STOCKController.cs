using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UluagroAspnet.Areas.AgroPark.ModelViews;
using UluagroAspnet.Models;

namespace UluagroAspnet.Areas.AgroPark.Controllers
{
    [AdminAuthenticationController]
    public class STOCKController : Controller
    {
        //type=1 'derman'
        //type=2 'gubre'

        OculusEntities db = new OculusEntities();

        // GET: AgroPark/STOCK
        public ActionResult Index()
        {
            var Stock = db.STOCKS.ToList();
            List<StockTotalView> stockView = new List<StockTotalView>();
            foreach (var item in db.STOCKS.ToList())
            {
                if (item.TYPE==1)
                {
                    stockView.Add(new StockTotalView() {
                        OBJECTID = item.OBJECTID,
                        CATEGORY="Dərman",
                        PRODUCTNAME = db.FERTILIZERs.Where(s => s.OBJECTID == item.PRODUCT_ID).FirstOrDefault().NAME + " (" + db.FERTILIZERs.Where(s => s.OBJECTID == item.PRODUCT_ID).FirstOrDefault().MAIN_COMPONENTS.NAME + ")",
                        QUANTITY = (decimal)item.QUANTITY
                    });
                }
                if (item.TYPE == 2)
                {
                    stockView.Add(new StockTotalView()
                    {
                        OBJECTID = item.OBJECTID,
                        CATEGORY = "Gübrə",
                        PRODUCTNAME = db.FERTILIZERs.Where(s => s.OBJECTID == item.PRODUCT_ID).FirstOrDefault().NAME + " (" + db.FERTILIZERs.Where(s => s.OBJECTID == item.PRODUCT_ID).FirstOrDefault().MAIN_COMPONENTS.NAME + ")",
                        QUANTITY = (decimal)item.QUANTITY
                    });
                }
                if (item.TYPE == 3)
                {
                    TechnicalSPEC techSpec= JsonConvert.DeserializeObject<TechnicalSPEC>(item.SPEC);
                    stockView.Add(new StockTotalView()
                    {
                        OBJECTID = item.OBJECTID,
                        CATEGORY = db.TECHNIQUES.Where(s => s.OBJECTID == item.PRODUCT_ID).FirstOrDefault().TECHNIQUE_TYPE.NAME,
                        PRODUCTNAME = db.TECHNIQUES.Where(s => s.OBJECTID == item.PRODUCT_ID).FirstOrDefault().NAME+" "+ techSpec.licenseplate,
                        QUANTITY = (decimal)item.QUANTITY
                    });
                }
                if (item.TYPE==4)
                {
                    CropSPEC _cropspec = JsonConvert.DeserializeObject<CropSPEC>(item.SPEC);
                    string CropName = db.CROP_SORT.Where(w => w.OBJECTID == _cropspec.CropSortID).FirstOrDefault().NAME;
                    string CropRepreduktion = db.CROP_REPRODUCTION.Where(s => s.OBJECTID == _cropspec.CropRepreducsiya).FirstOrDefault().NAME;
                    string _productname = db.CROPs.Where(s => s.OBJECTID == item.PRODUCT_ID).FirstOrDefault().NAME + "  -  " + CropName + " (" + CropRepreduktion + ")";

                    stockView.Add(new StockTotalView()
                    {
                        OBJECTID = item.OBJECTID,
                        CATEGORY = "Dənli bitki",
                        PRODUCTNAME = _productname,
                        QUANTITY = (decimal)item.QUANTITY
                    });
                }
                if (item.TYPE==5)
                {
                    CropSPEC _cropspec = JsonConvert.DeserializeObject<CropSPEC>(item.SPEC);
                    string CropName = db.CROP_SORT.Where(w => w.OBJECTID == _cropspec.CropSortID).FirstOrDefault().NAME;
                    string CropRepreduktion = db.CROP_REPRODUCTION.Where(s => s.OBJECTID == _cropspec.CropRepreducsiya).FirstOrDefault().NAME;
                    string _productname = db.CROPs.Where(s => s.OBJECTID == item.PRODUCT_ID).FirstOrDefault().NAME + "  -  " + CropName + " (" + CropRepreduktion + ")";

                    stockView.Add(new StockTotalView()
                    {
                        OBJECTID = item.OBJECTID,
                        CATEGORY = "Meyvə",
                        PRODUCTNAME = _productname,
                        QUANTITY = (decimal)item.QUANTITY
                    });
                }
            }

            return View(stockView);
        }

        public ActionResult Detail(int id)
        {
            STOCK _stockDetail= db.STOCKS.Find(id);
            List<StockModifyModel> vm = new List<StockModifyModel>();
            string _productname = "";
            if (_stockDetail.TYPE == 1 || _stockDetail.TYPE == 2)
            {
                _productname = db.FERTILIZERs.Where(s => s.OBJECTID == _stockDetail.PRODUCT_ID).FirstOrDefault().NAME;
            }
            if (_stockDetail.TYPE == 3)
            {
                TechnicalSPEC techSpec = JsonConvert.DeserializeObject<TechnicalSPEC>(_stockDetail.SPEC);
                _productname = db.TECHNIQUES.Where(s => s.OBJECTID == _stockDetail.PRODUCT_ID).FirstOrDefault().NAME + " " + techSpec.licenseplate;
            }
            if (_stockDetail.TYPE==4 || _stockDetail.TYPE==5)
            {
                CropSPEC cropSpec = JsonConvert.DeserializeObject<CropSPEC>(_stockDetail.SPEC);
                string CropName = db.CROP_SORT.Where(w => w.OBJECTID == cropSpec.CropSortID).FirstOrDefault().NAME;
                string CropRepreduktion = db.CROP_REPRODUCTION.Where(s => s.OBJECTID == cropSpec.CropRepreducsiya).FirstOrDefault().NAME;
                _productname = db.CROPs.Where(s => s.OBJECTID == _stockDetail.PRODUCT_ID).FirstOrDefault().NAME + "  -  " + CropName + " (" + CropRepreduktion + ")";

            }
            foreach (var item in db.STOCKMODIFies.Where(s=>s.STOCKITEMID==_stockDetail.OBJECTID).ToList())
            {
                vm.Add(new StockModifyModel()
                {
                    OBJECTID = item.OBJECTID,
                    PRODUCTNAME = _productname,
                    DESTINATIONSTATUS = (item.STATUSID == 1) ? "Medaxil" : "Mexaric",
                    MODIFYDATE = item.MODIFYDATE.Value.ToString("MM-dd-yyyy HH:mm"),
                    QUANTITY = item.QUANTITY.ToString(),
                    PLACENAME = item.STOCKPLACE.NAME,
                    WORKERNAME = item.Worker.Name + "  " + item.Worker.Surname
                });
            }

            return View(vm);
        }

        public ActionResult Details()
        {
            List<StockModifyModel> stvm = new List<StockModifyModel>();
            foreach (var item in db.STOCKMODIFies.ToList())
            {
                string _productname = "";
                if (item.STOCK.TYPE == 1 || item.STOCK.TYPE == 2)
                {
                    _productname = db.FERTILIZERs.Where(s => s.OBJECTID == item.STOCK.PRODUCT_ID).FirstOrDefault().NAME;
                }
                if (item.STOCK.TYPE==3)
                {
                    TechnicalSPEC techSpec = JsonConvert.DeserializeObject<TechnicalSPEC>(item.STOCK.SPEC);
                    _productname = db.TECHNIQUES.Where(s => s.OBJECTID == item.STOCK.PRODUCT_ID).FirstOrDefault().NAME + " " + techSpec.licenseplate;
                }
                if (item.STOCK.TYPE==4 || item.STOCK.TYPE==5)
                {
                    CropSPEC cropSpec = JsonConvert.DeserializeObject<CropSPEC>(item.STOCK.SPEC);
                    string CropName = db.CROP_SORT.Where(w => w.OBJECTID == cropSpec.CropSortID).FirstOrDefault().NAME;
                    string CropRepreduktion = db.CROP_REPRODUCTION.Where(s => s.OBJECTID == cropSpec.CropRepreducsiya).FirstOrDefault().NAME;
                    _productname = db.CROPs.Where(s => s.OBJECTID == item.STOCK.PRODUCT_ID).FirstOrDefault().NAME + "  -  " + CropName + " (" +CropRepreduktion + ")";
                }

                stvm.Add(new StockModifyModel()
                {
                    OBJECTID = item.OBJECTID,
                    PRODUCTNAME = _productname,
                    DESTINATIONSTATUS = (item.STATUSID==1) ? "Medaxil" : "Mexaric",
                    MODIFYDATE = item.MODIFYDATE.Value.ToString("MM-dd-yyyy HH:mm"),
                    QUANTITY = item.QUANTITY.ToString(),
                    PLACENAME=item.STOCKPLACE.NAME,
                    WORKERNAME=item.Worker.Name+"  "+item.Worker.Surname
                });
            }
            return View(stvm);
        }

        public ActionResult Create()
        {
            var logineduser = (User)Session["HaveLogin"];
            if (logineduser.UserAdmin_ID==1)
            {
                return RedirectToAction("Index", "STOCK");
            }
            ViewBag.Techicals = db.TECHNIQUE_TYPE.ToList();
            ViewBag.STOCKPLACES = db.STOCKPLACES.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(StockViewModel _stock, List<string> SPECS)
        {
            if (Session["HaveLogin"]==null)
            {
                return RedirectToAction("Index", "Login");
            }
            var logineduser = (User)Session["HaveLogin"];
            
            if (true)
            {
                if (_stock.TYPE==1 || _stock.TYPE==2)
                {
                    if (_stock.STATUSID == 1)
                    {
                        var exsistItem = db.STOCKS.Where(s => s.PRODUCT_ID == _stock.PRODUCT_ID).FirstOrDefault();
                        if (exsistItem == null)
                        {
                            STOCK _newStock = new STOCK();
                            _newStock.PRODUCT_ID = _stock.PRODUCT_ID;
                            _newStock.TYPE = (short)_stock.TYPE;
                            _newStock.QUANTITY = _stock.QUANTITY;
                            _newStock.SPEC = "";
                            db.STOCKS.Add(_newStock);
                            db.SaveChanges();

                            STOCKMODIFY _newmodify = new STOCKMODIFY();
                            _newmodify.STOCKITEMID = _newStock.OBJECTID;
                            _newmodify.STATUSID = _stock.STATUSID;
                            _newmodify.MODIFYDATE = DateTime.Now;
                            _newmodify.QUANTITY = _stock.QUANTITY;
                            _newmodify.PLACEID = _stock.PLACEID;
                            _newmodify.WORKERID = logineduser.Worker.ID;
                            db.STOCKMODIFies.Add(_newmodify);
                            db.SaveChanges();
                            return RedirectToAction("Index", "STOCK");
                        }
                        else
                        {                            
                            exsistItem.QUANTITY += _stock.QUANTITY;
                            db.Entry(exsistItem).State = EntityState.Modified;
                            db.SaveChanges();

                            STOCKMODIFY _newmodify = new STOCKMODIFY();
                            _newmodify.STOCKITEMID = exsistItem.OBJECTID;
                            _newmodify.STATUSID = _stock.STATUSID;
                            _newmodify.MODIFYDATE = DateTime.Now;
                            _newmodify.QUANTITY = _stock.QUANTITY;
                            _newmodify.PLACEID = _stock.PLACEID;
                            _newmodify.WORKERID = logineduser.Worker.ID;
                            db.STOCKMODIFies.Add(_newmodify);
                            db.SaveChanges();
                            return RedirectToAction("Index", "STOCK");
                            
                            
                        }
                    }
                    if (_stock.STATUSID == 2)
                    {
                        var exsistItem = db.STOCKS.Where(s => s.OBJECTID == _stock.PRODUCT_ID).FirstOrDefault();
                        if (exsistItem.QUANTITY >= _stock.QUANTITY)
                        {
                            exsistItem.QUANTITY -= _stock.QUANTITY;
                            db.Entry(exsistItem).State = EntityState.Modified;
                            db.SaveChanges();

                            STOCKMODIFY _newmodify = new STOCKMODIFY();
                            _newmodify.STOCKITEMID = exsistItem.OBJECTID;
                            _newmodify.STATUSID = _stock.STATUSID;
                            _newmodify.MODIFYDATE = DateTime.Now;
                            _newmodify.QUANTITY = _stock.QUANTITY;
                            _newmodify.PLACEID = _stock.PLACEID;
                            _newmodify.WORKERID = logineduser.Worker.ID;
                            db.STOCKMODIFies.Add(_newmodify);
                            db.SaveChanges();
                            return RedirectToAction("Index", "STOCK");
                        }
                        else
                        {
                            //anbarda kifayyet qeder ehtiyat yoxdu.
                            //not enough product in stock
                            ViewBag.Techicals = db.TECHNIQUE_TYPE.ToList();
                            ViewBag.STOCKPLACES = db.STOCKPLACES.ToList();
                            return View();
                        }
                    }

                }
                if (_stock.TYPE == 3)
                {
                    if (_stock.STATUSID==1)
                    {
                        STOCK _newStock = new STOCK();
                        _newStock.PRODUCT_ID = _stock.PRODUCT_ID;
                        _newStock.TYPE = (short)_stock.TYPE;
                        _newStock.QUANTITY = _stock.QUANTITY;

                        TechnicalSPEC techspec = new TechnicalSPEC();
                        techspec.licenseplate = SPECS[0];
                        techspec.color = SPECS[1];
                        var ResuptSpec= JsonConvert.SerializeObject(techspec);
                        _newStock.SPEC = ResuptSpec;
                        db.STOCKS.Add(_newStock);
                        db.SaveChanges();

                        STOCKMODIFY _newmodify = new STOCKMODIFY();
                        _newmodify.STOCKITEMID = _newStock.OBJECTID;
                        _newmodify.STATUSID = _stock.STATUSID;
                        _newmodify.MODIFYDATE = DateTime.Now;
                        _newmodify.QUANTITY = _stock.QUANTITY;
                        _newmodify.PLACEID = _stock.PLACEID;
                        _newmodify.WORKERID = logineduser.Worker.ID;
                        db.STOCKMODIFies.Add(_newmodify);
                        db.SaveChanges();
                        return RedirectToAction("Index", "STOCK");
                    }
                    if (_stock.STATUSID == 2)
                    {
                        STOCK exsist= db.STOCKS.Find(_stock.PRODUCT_ID);
                        exsist.QUANTITY -= 1;
                        db.SaveChanges();

                        STOCKMODIFY _newmodify = new STOCKMODIFY();
                        _newmodify.STOCKITEMID = exsist.OBJECTID;
                        _newmodify.STATUSID = _stock.STATUSID;
                        _newmodify.MODIFYDATE = DateTime.Now;
                        _newmodify.QUANTITY = _stock.QUANTITY;
                        _newmodify.PLACEID = _stock.PLACEID;
                        _newmodify.WORKERID = logineduser.Worker.ID;
                        db.STOCKMODIFies.Add(_newmodify);
                        db.SaveChanges();
                        return RedirectToAction("Index", "STOCK");

                    }


                    return Content("test true");
                }
                if (_stock.TYPE==4 || _stock.TYPE==5)
                {
                    //medaxil olanda
                    if (_stock.STATUSID==1)
                    {
                        int cropID = Convert.ToInt32(SPECS[0]);
                        int CropSortID = Convert.ToInt32(SPECS[1]);
                        CropSPEC cropspec = new CropSPEC();
                        cropspec.CropSortID = _stock.PRODUCT_ID;
                        cropspec.CropRepreducsiya = CropSortID;
                        string CropSpecial = JsonConvert.SerializeObject(cropspec);

                        var exsistItem = db.STOCKS.Where(s => s.PRODUCT_ID == cropID && s.SPEC==CropSpecial).FirstOrDefault();
                        //eger anbarda yolxdursa yenisin elave edir
                        if (exsistItem==null)
                        {
                            
                            STOCK newCropStok = new STOCK();
                            newCropStok.PRODUCT_ID = cropID;
                            newCropStok.TYPE =(short)_stock.TYPE;
                            newCropStok.QUANTITY = _stock.QUANTITY;
                            newCropStok.SPEC = CropSpecial;
                            db.STOCKS.Add(newCropStok);
                            db.SaveChanges();

                            STOCKMODIFY _newmodify = new STOCKMODIFY();
                            _newmodify.STOCKITEMID = newCropStok.OBJECTID;
                            _newmodify.STATUSID = _stock.STATUSID;
                            _newmodify.MODIFYDATE = DateTime.Now;
                            _newmodify.QUANTITY = _stock.QUANTITY;
                            _newmodify.PLACEID = _stock.PLACEID;
                            _newmodify.WORKERID = logineduser.Worker.ID;
                            db.STOCKMODIFies.Add(_newmodify);
                            db.SaveChanges();
                            return RedirectToAction("Index", "STOCK");

                        }
                        else// eger umumi anbarda bele bir mehsul varsa artiq onun uzerine elave edir.
                        {
                            exsistItem.QUANTITY += _stock.QUANTITY;
                            db.Entry(exsistItem).State = EntityState.Modified;
                            db.SaveChanges();

                            STOCKMODIFY _newmodify = new STOCKMODIFY();
                            _newmodify.STOCKITEMID = exsistItem.OBJECTID;
                            _newmodify.STATUSID = _stock.STATUSID;
                            _newmodify.MODIFYDATE = DateTime.Now;
                            _newmodify.QUANTITY = _stock.QUANTITY;
                            _newmodify.PLACEID = _stock.PLACEID;
                            _newmodify.WORKERID = logineduser.Worker.ID;
                            db.STOCKMODIFies.Add(_newmodify);
                            db.SaveChanges();
                            return RedirectToAction("Index", "STOCK");
                        }
                    }
                    if (_stock.STATUSID==2)
                    {
                        var exsistItem = db.STOCKS.Where(s => s.OBJECTID == _stock.PRODUCT_ID).FirstOrDefault();
                        if (exsistItem.QUANTITY >= _stock.QUANTITY)
                        {
                            exsistItem.QUANTITY -= _stock.QUANTITY;
                            db.Entry(exsistItem).State = EntityState.Modified;
                            db.SaveChanges();

                            STOCKMODIFY _newmodify = new STOCKMODIFY();
                            _newmodify.STOCKITEMID = exsistItem.OBJECTID;
                            _newmodify.STATUSID = _stock.STATUSID;
                            _newmodify.MODIFYDATE = DateTime.Now;
                            _newmodify.QUANTITY = _stock.QUANTITY;
                            _newmodify.PLACEID = _stock.PLACEID;
                            _newmodify.WORKERID = logineduser.Worker.ID;
                            db.STOCKMODIFies.Add(_newmodify);
                            db.SaveChanges();
                            return RedirectToAction("Index", "STOCK");
                        }
                        else
                        {
                            //anbarda kifayyet qeder ehtiyat yoxdu.
                            //not enough product in stock
                            ViewBag.Techicals = db.TECHNIQUE_TYPE.ToList();
                            ViewBag.STOCKPLACES = db.STOCKPLACES.ToList();
                            return View();
                        }
                    }
                }
            }
            ViewBag.Techicals = db.TECHNIQUE_TYPE.ToList();
            ViewBag.STOCKPLACES = db.STOCKPLACES.ToList();
            return View();
        }


        //Stock Ajaxs Requests
        public ActionResult GetFeltizilerByCATID(int catid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<AllFeltilizerGet> _allFeltilizers = db.FERTILIZERs.Where(s=>s.CATEGORY_ID==catid).Select(s=>new AllFeltilizerGet {
                OBJECTID=s.OBJECTID,
                NAME=s.NAME + " ("+ db.MAIN_COMPONENTS.Where(w=>w.OBJECTID==s.MAIN_COMPONENT_ID).FirstOrDefault().NAME + ")"
            }).ToList();           
            return Json(_allFeltilizers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTechnical(int techcatid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<TECHNIQUE> _allTechnicals = db.TECHNIQUES.Where(s=>s.TYPE_ID==techcatid).ToList();
            return Json(_allTechnicals, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStockResursTech(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<ExitStockModel> vm = new List<ExitStockModel>();
            foreach (var item in db.STOCKS.Where(w => w.TYPE == id && w.QUANTITY > 0))
            {
                ExitStockModel onestock = new ExitStockModel();
                onestock.OBJECTID = item.OBJECTID;
                onestock.PRODUCTNAME = db.TECHNIQUES.Where(t => t.OBJECTID == item.PRODUCT_ID).FirstOrDefault().NAME +" " + JsonConvert.DeserializeObject<TechnicalSPEC>(item.SPEC).licenseplate;
                onestock.TOTAL = item.QUANTITY.ToString();
                vm.Add(onestock);
            };            
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StockPlaces(int id)
        {
            List<StockPlacesAndQuantity> stocks = new List<StockPlacesAndQuantity>();
            if (id==0)
            {
                db.Configuration.ProxyCreationEnabled = false;
                stocks = db.STOCKPLACES.Select(s=>new StockPlacesAndQuantity() {
                    OBJECTID=s.OBJECTID,
                    STOCKPLACENAME=s.NAME
                }).ToList();
            }
            else
            {
                //id stock tablasinde olan mehsullarin object id-si olaraq gelir.
                List<STOCKMODIFY> stockAll = db.STOCKMODIFies.Where(s => s.STOCKITEMID == id && s.STOCK.QUANTITY > 0).ToList();
                var input = stockAll.GroupBy(p => p.PLACEID, p => p.QUANTITY, (key, g) => new { placeid = key, total = g.Sum() });
                foreach (var item in input)
                {
                    StockPlacesAndQuantity onestock = new StockPlacesAndQuantity();
                    onestock.OBJECTID = (int)item.placeid;
                    onestock.STOCKPLACENAME = db.STOCKPLACES.Where(w => w.OBJECTID == item.placeid).FirstOrDefault().NAME;
                    decimal output = 0;
                    if (db.STOCKMODIFies.Where(s => s.STOCKITEMID == id && s.PLACEID == item.placeid && s.STATUSID == 2).Sum(s => s.QUANTITY)!=null)
                    {
                        output = (decimal)db.STOCKMODIFies.Where(s => s.STOCKITEMID == id && s.PLACEID == item.placeid && s.STATUSID == 2).Sum(s => s.QUANTITY);
                    }
                    onestock.QUANTITYTOTAL = (decimal)(item.total - output);
                    stocks.Add(onestock);
                }
                //List<STOCKMODIFY> stok = db.STOCKMODIFies.Where(s => s.STOCK.OBJECTID == id && s.STOCK.QUANTITY > 0).ToList();
                //foreach (var item in stok)
                //{                    
                //  STOCKPLACE onestock = new STOCKPLACE();
                //onestock.OBJECTID = db.STOCKPLACES.Where(w => w.OBJECTID == item.PLACEID).FirstOrDefault().OBJECTID;
                //onestock.NAME= db.STOCKPLACES.Where(w => w.OBJECTID == item.PLACEID).FirstOrDefault().NAME;
                //_stockplaces.Add(onestock);
                //}
            }
            return Json(stocks, JsonRequestBehavior.AllowGet);
        }       

        public ActionResult GetStockResursFelt(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<ExitStockModel> vm = new List<ExitStockModel>();
            foreach (var item in db.STOCKS.Where(w => w.TYPE == id && w.QUANTITY > 0))
            {
                ExitStockModel onestock = new ExitStockModel();
                onestock.OBJECTID = item.OBJECTID;
                onestock.PRODUCTNAME = db.FERTILIZERs.Where(t => t.OBJECTID == item.PRODUCT_ID).FirstOrDefault().NAME;
                onestock.TOTAL = item.QUANTITY.ToString();
                vm.Add(onestock);
            };
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        //mehsulun categoriyasina gore bolme (bitki ve ya meyve olmasi)
        public ActionResult GetAllCropsByCatID(int id)
        {
            List<CROP> crops = new List<CROP>();
            if (id==4)
            {
                db.Configuration.ProxyCreationEnabled = false;
                crops = db.CROPs.Where(d => d.PARCEL_CATEGORY_ID == 1).ToList();
                
            }
            if (id==5)
            {
                db.Configuration.ProxyCreationEnabled = false;
                crops = db.CROPs.Where(d => d.PARCEL_CATEGORY_ID == 2).ToList();
            }

            return Json(crops, JsonRequestBehavior.AllowGet);

        }

        // bitki ve meyvelerin sorlari
        public ActionResult GelSortByCropID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<CROP_SORT> cropsots = db.CROP_SORT.Where(s => s.CROP_ID == id).ToList();

            return Json(cropsots, JsonRequestBehavior.AllowGet);
        }

        //repreduksiysi
        public ActionResult GetCropRepreducsiya()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<CROP_REPRODUCTION> crop_repreduc = db.CROP_REPRODUCTION.ToList();
            return Json(crop_repreduc, JsonRequestBehavior.AllowGet);
        }

        //anbarda olan bitki ve meyvelerin siyahisi
        public ActionResult GetOutCropStok(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<ExitStockModel> vm = new List<ExitStockModel>();
            foreach (var item in db.STOCKS.Where(w => w.TYPE == id && w.QUANTITY > 0))
            {
                CropSPEC _cropSpecial = JsonConvert.DeserializeObject<CropSPEC>(item.SPEC);
                string cropName = db.CROPs.Where(t => t.OBJECTID == item.PRODUCT_ID).FirstOrDefault().NAME;
                string SortName = db.CROP_SORT.Where(s => s.OBJECTID == _cropSpecial.CropSortID).FirstOrDefault().NAME;
                string RepreducName = db.CROP_REPRODUCTION.Where(s => s.OBJECTID == _cropSpecial.CropRepreducsiya).FirstOrDefault().NAME;
                ExitStockModel onestock = new ExitStockModel();
                onestock.OBJECTID = item.OBJECTID;
                onestock.PRODUCTNAME =cropName  +" - " + SortName + "  ("+RepreducName + ")" ;
                onestock.TOTAL = item.QUANTITY.ToString();
                vm.Add(onestock);
            };
            return Json(vm, JsonRequestBehavior.AllowGet);
        }




    }
}