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
    public class FIRMsController : Controller
    {
        private OculusEntities db = new OculusEntities();

        // GET: AgroPark/FIRMs
        public ActionResult Index()
        {
            return View(db.FIRMS.ToList());
        }

        
        // GET: AgroPark/FIRMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgroPark/FIRMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OBJECTID,NAME")] FIRM fIRM)
        {
            if (ModelState.IsValid)
            {
                db.FIRMS.Add(fIRM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fIRM);
        }

        // GET: AgroPark/FIRMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FIRM fIRM = db.FIRMS.Find(id);
            if (fIRM == null)
            {
                return HttpNotFound();
            }
            return View(fIRM);
        }

        // POST: AgroPark/FIRMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OBJECTID,NAME")] FIRM fIRM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fIRM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fIRM);
        }

        // GET: AgroPark/FIRMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FIRM fIRM = db.FIRMS.Find(id);
            if (fIRM == null)
            {
                return HttpNotFound();
            }
            return View(fIRM);
        }

        // POST: AgroPark/FIRMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FIRM fIRM = db.FIRMS.Find(id);
            db.FIRMS.Remove(fIRM);
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

        public void AddFirm(string name)
        {
            FIRM new_firm = new FIRM
            {
                NAME = name
            };

            db.FIRMS.Add(new_firm);
            db.SaveChanges();
        }

        public void EditFirm(int id, string name)
        {
            FIRM slc_firm = db.FIRMS.Find(id);

            slc_firm.NAME = name;
            db.SaveChanges();
        }
    }
}
