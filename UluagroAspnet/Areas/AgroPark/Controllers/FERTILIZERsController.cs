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
    public class FERTILIZERsController : Controller
    {
        private OculusEntities db = new OculusEntities();

        // GET: AgroPark/FERTILIZERs
        public ActionResult Index()
        {
            var fERTILIZERs = db.FERTILIZERs.Include(f => f.CATEGORy).Include(f => f.MAIN_COMPONENTS).Include(f => f.FIRM).Include(f => f.MARK).Include(f => f.PACKING).Include(f => f.UNIT);
            return View(fERTILIZERs.ToList());
        }

        
        // GET: AgroPark/FERTILIZERs/Create
        public ActionResult Create()
        {
            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "OBJECTID", "NAME");
            ViewBag.MAIN_COMPONENT_ID = new SelectList(db.MAIN_COMPONENTS, "OBJECTID", "NAME");
            ViewBag.FIRM_ID = new SelectList(db.FIRMS, "OBJECTID", "NAME");
            ViewBag.MARK_ID = new SelectList(db.MARKS, "OBJECTID", "NAME");
            ViewBag.PACKING_ID = new SelectList(db.PACKINGs, "OBJECTID", "NAME");
            ViewBag.UNIT_TYPE_ID = new SelectList(db.UNITS, "OBJECTID", "NAME");
            return View();
        }

        // POST: AgroPark/FERTILIZERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OBJECTID,FIRM_ID,MARK_ID,MAIN_COMPONENT_ID,PACKING_ID,UNIT_TYPE_ID,WATER_PER_KG,NAME,CATEGORY_ID")] FERTILIZER fERTILIZER)
        {
            if (ModelState.IsValid)
            {
                db.FERTILIZERs.Add(fERTILIZER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "OBJECTID", "NAME", fERTILIZER.CATEGORY_ID);
            ViewBag.MAIN_COMPONENT_ID = new SelectList(db.MAIN_COMPONENTS, "OBJECTID", "NAME", fERTILIZER.MAIN_COMPONENT_ID);
            ViewBag.FIRM_ID = new SelectList(db.FIRMS, "OBJECTID", "NAME", fERTILIZER.FIRM_ID);
            ViewBag.MARK_ID = new SelectList(db.MARKS, "OBJECTID", "NAME", fERTILIZER.MARK_ID);
            ViewBag.PACKING_ID = new SelectList(db.PACKINGs, "OBJECTID", "NAME", fERTILIZER.PACKING_ID);
            ViewBag.UNIT_TYPE_ID = new SelectList(db.UNITS, "OBJECTID", "NAME", fERTILIZER.UNIT_TYPE_ID);
            return View(fERTILIZER);
        }

        // GET: AgroPark/FERTILIZERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FERTILIZER fERTILIZER = db.FERTILIZERs.Find(id);
            if (fERTILIZER == null)
            {
                return HttpNotFound();
            }
            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "OBJECTID", "NAME", fERTILIZER.CATEGORY_ID);
            ViewBag.MAIN_COMPONENT_ID = new SelectList(db.MAIN_COMPONENTS, "OBJECTID", "NAME", fERTILIZER.MAIN_COMPONENT_ID);
            ViewBag.FIRM_ID = new SelectList(db.FIRMS, "OBJECTID", "NAME", fERTILIZER.FIRM_ID);
            ViewBag.MARK_ID = new SelectList(db.MARKS, "OBJECTID", "NAME", fERTILIZER.MARK_ID);
            ViewBag.PACKING_ID = new SelectList(db.PACKINGs, "OBJECTID", "NAME", fERTILIZER.PACKING_ID);
            ViewBag.UNIT_TYPE_ID = new SelectList(db.UNITS, "OBJECTID", "NAME", fERTILIZER.UNIT_TYPE_ID);
            return View(fERTILIZER);
        }

        // POST: AgroPark/FERTILIZERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OBJECTID,FIRM_ID,MARK_ID,MAIN_COMPONENT_ID,PACKING_ID,UNIT_TYPE_ID,WATER_PER_KG,NAME,CATEGORY_ID")] FERTILIZER fERTILIZER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fERTILIZER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "OBJECTID", "NAME", fERTILIZER.CATEGORY_ID);
            ViewBag.MAIN_COMPONENT_ID = new SelectList(db.MAIN_COMPONENTS, "OBJECTID", "NAME", fERTILIZER.MAIN_COMPONENT_ID);
            ViewBag.FIRM_ID = new SelectList(db.FIRMS, "OBJECTID", "NAME", fERTILIZER.FIRM_ID);
            ViewBag.MARK_ID = new SelectList(db.MARKS, "OBJECTID", "NAME", fERTILIZER.MARK_ID);
            ViewBag.PACKING_ID = new SelectList(db.PACKINGs, "OBJECTID", "NAME", fERTILIZER.PACKING_ID);
            ViewBag.UNIT_TYPE_ID = new SelectList(db.UNITS, "OBJECTID", "NAME", fERTILIZER.UNIT_TYPE_ID);
            return View(fERTILIZER);
        }

        // GET: AgroPark/FERTILIZERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FERTILIZER fERTILIZER = db.FERTILIZERs.Find(id);
            if (fERTILIZER == null)
            {
                return HttpNotFound();
            }
            return View(fERTILIZER);
        }

        // POST: AgroPark/FERTILIZERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FERTILIZER fERTILIZER = db.FERTILIZERs.Find(id);
            db.FERTILIZERs.Remove(fERTILIZER);
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
