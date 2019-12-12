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
    public class WORKsController : Controller
    {
        private OculusEntities db = new OculusEntities();

        // GET: AgroPark/WORKs
        public ActionResult Index()
        {
            var wORKS = db.WORKS.Include(w => w.WORKS_CATEGORY);
            return View(wORKS.ToList());
        }        

        // GET: AgroPark/WORKs/Create
        public ActionResult Create()
        {
            ViewBag.WORK_CAT_ID = new SelectList(db.WORKS_CATEGORY, "OBJECTID", "NAME");
            return View();
        }

        // POST: AgroPark/WORKs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OBJECTID,NAME,WORK_CAT_ID")] WORK wORK)
        {
            if (ModelState.IsValid)
            {
                db.WORKS.Add(wORK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WORK_CAT_ID = new SelectList(db.WORKS_CATEGORY, "OBJECTID", "NAME", wORK.WORK_CAT_ID);
            return View(wORK);
        }

        // GET: AgroPark/WORKs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WORK wORK = db.WORKS.Find(id);
            if (wORK == null)
            {
                return HttpNotFound();
            }
            ViewBag.WORK_CAT_ID = new SelectList(db.WORKS_CATEGORY, "OBJECTID", "NAME", wORK.WORK_CAT_ID);
            return View(wORK);
        }

        // POST: AgroPark/WORKs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OBJECTID,NAME,WORK_CAT_ID")] WORK wORK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wORK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WORK_CAT_ID = new SelectList(db.WORKS_CATEGORY, "OBJECTID", "NAME", wORK.WORK_CAT_ID);
            return View(wORK);
        }

        // GET: AgroPark/WORKs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WORK wORK = db.WORKS.Find(id);
            if (wORK == null)
            {
                return HttpNotFound();
            }
            return View(wORK);
        }

        // POST: AgroPark/WORKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WORK wORK = db.WORKS.Find(id);
            db.WORKS.Remove(wORK);
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
