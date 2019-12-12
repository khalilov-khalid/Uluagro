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
    public class UNITsController : Controller
    {
        private OculusEntities db = new OculusEntities();

        // GET: AgroPark/UNITs
        public ActionResult Index()
        {
            return View(db.UNITS.ToList());
        }

        
        // GET: AgroPark/UNITs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgroPark/UNITs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OBJECTID,NAME")] UNIT uNIT)
        {
            if (ModelState.IsValid)
            {
                db.UNITS.Add(uNIT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uNIT);
        }

        // GET: AgroPark/UNITs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UNIT uNIT = db.UNITS.Find(id);
            if (uNIT == null)
            {
                return HttpNotFound();
            }
            return View(uNIT);
        }

        // POST: AgroPark/UNITs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OBJECTID,NAME")] UNIT uNIT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uNIT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uNIT);
        }

        // GET: AgroPark/UNITs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UNIT uNIT = db.UNITS.Find(id);
            if (uNIT == null)
            {
                return HttpNotFound();
            }
            return View(uNIT);
        }

        // POST: AgroPark/UNITs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UNIT uNIT = db.UNITS.Find(id);
            db.UNITS.Remove(uNIT);
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

        public void AddUnit(string name)
        {
            UNIT new_unit = new UNIT
            {
                NAME = name
            };

            db.UNITS.Add(new_unit);
            db.SaveChanges();
        }

        public void EditUnit(int id, string name)
        {
            UNIT slctd_unit = db.UNITS.Find(id);

            slctd_unit.NAME = name;
            db.SaveChanges();
        }
    }
}
