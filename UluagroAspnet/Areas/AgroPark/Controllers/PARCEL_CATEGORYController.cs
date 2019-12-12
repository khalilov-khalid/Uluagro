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
    public class PARCEL_CATEGORYController : Controller
    {
        private OculusEntities db = new OculusEntities();

        // GET: AgroPark/PARCEL_CATEGORY
        public ActionResult Index()
        {
            return View(db.PARCEL_CATEGORY.ToList());
        }                

        // GET: AgroPark/PARCEL_CATEGORY/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgroPark/PARCEL_CATEGORY/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OBJECTID,NAME")] PARCEL_CATEGORY pARCEL_CATEGORY)
        {
            if (ModelState.IsValid)
            {
                db.PARCEL_CATEGORY.Add(pARCEL_CATEGORY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pARCEL_CATEGORY);
        }

        // GET: AgroPark/PARCEL_CATEGORY/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARCEL_CATEGORY pARCEL_CATEGORY = db.PARCEL_CATEGORY.Find(id);
            if (pARCEL_CATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(pARCEL_CATEGORY);
        }

        // POST: AgroPark/PARCEL_CATEGORY/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OBJECTID,NAME")] PARCEL_CATEGORY pARCEL_CATEGORY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pARCEL_CATEGORY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pARCEL_CATEGORY);
        }

        // GET: AgroPark/PARCEL_CATEGORY/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARCEL_CATEGORY pARCEL_CATEGORY = db.PARCEL_CATEGORY.Find(id);
            if (pARCEL_CATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(pARCEL_CATEGORY);
        }

        // POST: AgroPark/PARCEL_CATEGORY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PARCEL_CATEGORY pARCEL_CATEGORY = db.PARCEL_CATEGORY.Find(id);
            db.PARCEL_CATEGORY.Remove(pARCEL_CATEGORY);
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
