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
    public class TECHNIQUEsController : Controller
    {
        private OculusEntities db = new OculusEntities();

        // GET: AgroPark/TECHNIQUEs
        public ActionResult Index()
        {
            var tECHNIQUES = db.TECHNIQUES.Include(t => t.TECHNIQUE_CONDITION).Include(t => t.TECHNIQUE_TYPE).Include(t => t.TECHNIQUE_WORKING_TYPE);
            return View(tECHNIQUES.ToList());
        }
        

        // GET: AgroPark/TECHNIQUEs/Create
        public ActionResult Create()
        {
            ViewBag.CONDITION_ID = new SelectList(db.TECHNIQUE_CONDITION, "OBJECTID", "NAME");
            ViewBag.TYPE_ID = new SelectList(db.TECHNIQUE_TYPE, "OBJECTID", "NAME");
            ViewBag.WORKING_TYPE_ID = new SelectList(db.TECHNIQUE_WORKING_TYPE, "OBJECTID", "NAME");
            return View();
        }

        // POST: AgroPark/TECHNIQUEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OBJECTID,NAME,TYPE_ID,CONDITION_ID,WORKING_TYPE_ID")] TECHNIQUE tECHNIQUE)
        {
            if (ModelState.IsValid)
            {
                db.TECHNIQUES.Add(tECHNIQUE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CONDITION_ID = new SelectList(db.TECHNIQUE_CONDITION, "OBJECTID", "NAME", tECHNIQUE.CONDITION_ID);
            ViewBag.TYPE_ID = new SelectList(db.TECHNIQUE_TYPE, "OBJECTID", "NAME", tECHNIQUE.TYPE_ID);
            ViewBag.WORKING_TYPE_ID = new SelectList(db.TECHNIQUE_WORKING_TYPE, "OBJECTID", "NAME", tECHNIQUE.WORKING_TYPE_ID);
            return View(tECHNIQUE);
        }

        // GET: AgroPark/TECHNIQUEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TECHNIQUE tECHNIQUE = db.TECHNIQUES.Find(id);
            if (tECHNIQUE == null)
            {
                return HttpNotFound();
            }
            ViewBag.CONDITION_ID = new SelectList(db.TECHNIQUE_CONDITION, "OBJECTID", "NAME", tECHNIQUE.CONDITION_ID);
            ViewBag.TYPE_ID = new SelectList(db.TECHNIQUE_TYPE, "OBJECTID", "NAME", tECHNIQUE.TYPE_ID);
            ViewBag.WORKING_TYPE_ID = new SelectList(db.TECHNIQUE_WORKING_TYPE, "OBJECTID", "NAME", tECHNIQUE.WORKING_TYPE_ID);
            return View(tECHNIQUE);
        }

        // POST: AgroPark/TECHNIQUEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OBJECTID,NAME,TYPE_ID,CONDITION_ID,WORKING_TYPE_ID")] TECHNIQUE tECHNIQUE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tECHNIQUE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CONDITION_ID = new SelectList(db.TECHNIQUE_CONDITION, "OBJECTID", "NAME", tECHNIQUE.CONDITION_ID);
            ViewBag.TYPE_ID = new SelectList(db.TECHNIQUE_TYPE, "OBJECTID", "NAME", tECHNIQUE.TYPE_ID);
            ViewBag.WORKING_TYPE_ID = new SelectList(db.TECHNIQUE_WORKING_TYPE, "OBJECTID", "NAME", tECHNIQUE.WORKING_TYPE_ID);
            return View(tECHNIQUE);
        }

        // GET: AgroPark/TECHNIQUEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TECHNIQUE tECHNIQUE = db.TECHNIQUES.Find(id);
            if (tECHNIQUE == null)
            {
                return HttpNotFound();
            }
            return View(tECHNIQUE);
        }

        // POST: AgroPark/TECHNIQUEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TECHNIQUE tECHNIQUE = db.TECHNIQUES.Find(id);
            db.TECHNIQUES.Remove(tECHNIQUE);
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
