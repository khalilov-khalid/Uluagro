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
    public class STOCKPLACEsController : Controller
    {
        private OculusEntities db = new OculusEntities();

        // GET: AgroPark/STOCKPLACEs
        public ActionResult Index()
        {
            return View(db.STOCKPLACES.ToList());
        }        

        // GET: AgroPark/STOCKPLACEs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgroPark/STOCKPLACEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OBJECTID,NAME")] STOCKPLACE sTOCKPLACE)
        {
            if (ModelState.IsValid)
            {
                db.STOCKPLACES.Add(sTOCKPLACE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sTOCKPLACE);
        }

        // GET: AgroPark/STOCKPLACEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STOCKPLACE sTOCKPLACE = db.STOCKPLACES.Find(id);
            if (sTOCKPLACE == null)
            {
                return HttpNotFound();
            }
            return View(sTOCKPLACE);
        }

        // POST: AgroPark/STOCKPLACEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OBJECTID,NAME")] STOCKPLACE sTOCKPLACE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTOCKPLACE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sTOCKPLACE);
        }

        // GET: AgroPark/STOCKPLACEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STOCKPLACE sTOCKPLACE = db.STOCKPLACES.Find(id);
            if (sTOCKPLACE == null)
            {
                return HttpNotFound();
            }
            return View(sTOCKPLACE);
        }

        // POST: AgroPark/STOCKPLACEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            STOCKPLACE sTOCKPLACE = db.STOCKPLACES.Find(id);
            db.STOCKPLACES.Remove(sTOCKPLACE);
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
