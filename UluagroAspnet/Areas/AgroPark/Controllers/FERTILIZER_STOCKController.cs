using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UluagroAspnet.Models;

namespace UluagroAspnet.Areas.AgroPark.Controllers
{
    [AdminAuthenticationController]
    public class FERTILIZER_STOCKController : Controller
    {
        private OculusEntities db = new OculusEntities();

        // GET: AgroPark/FERTILIZER_STOCK
        public ActionResult Index()
        {
            var fERTILIZER_STOCK = db.FERTILIZER_STOCK.Include(f => f.FERTILIZER);
            return View(fERTILIZER_STOCK.ToList());
        }

        
        // GET: AgroPark/FERTILIZER_STOCK/Create
        public ActionResult Create()
        {
            ViewBag.FERTILIZER_ID = new SelectList(db.FERTILIZERs, "OBJECTID", "NAME");
            return View();
        }

        // POST: AgroPark/FERTILIZER_STOCK/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OBJECTID,FERTILIZER_ID,UNIT,USED_DATE")] FERTILIZER_STOCK fERTILIZER_STOCK)
        {
            if (ModelState.IsValid)
            {
                fERTILIZER_STOCK.CREATED_DATE = DateTime.Now;
                db.FERTILIZER_STOCK.Add(fERTILIZER_STOCK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FERTILIZER_ID = new SelectList(db.FERTILIZERs, "OBJECTID", "NAME", fERTILIZER_STOCK.FERTILIZER_ID);
            return View(fERTILIZER_STOCK);
        }

        // GET: AgroPark/FERTILIZER_STOCK/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FERTILIZER_STOCK fERTILIZER_STOCK = db.FERTILIZER_STOCK.Find(id);
            if (fERTILIZER_STOCK == null)
            {
                return HttpNotFound();
            }
            ViewBag.FERTILIZER_ID = new SelectList(db.FERTILIZERs, "OBJECTID", "NAME", fERTILIZER_STOCK.FERTILIZER_ID);
            return View(fERTILIZER_STOCK);
        }

        // POST: AgroPark/FERTILIZER_STOCK/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OBJECTID,FERTILIZER_ID,UNIT,CREATED_DATE,USED_DATE")] FERTILIZER_STOCK fERTILIZER_STOCK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fERTILIZER_STOCK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FERTILIZER_ID = new SelectList(db.FERTILIZERs, "OBJECTID", "NAME", fERTILIZER_STOCK.FERTILIZER_ID);
            return View(fERTILIZER_STOCK);
        }

        // GET: AgroPark/FERTILIZER_STOCK/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FERTILIZER_STOCK fERTILIZER_STOCK = db.FERTILIZER_STOCK.Find(id);
            if (fERTILIZER_STOCK == null)
            {
                return HttpNotFound();
            }
            return View(fERTILIZER_STOCK);
        }

        // POST: AgroPark/FERTILIZER_STOCK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FERTILIZER_STOCK fERTILIZER_STOCK = db.FERTILIZER_STOCK.Find(id);
            db.FERTILIZER_STOCK.Remove(fERTILIZER_STOCK);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
