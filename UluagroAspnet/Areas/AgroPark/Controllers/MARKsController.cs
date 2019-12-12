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
    public class MARKsController : Controller
    {
        private OculusEntities db = new OculusEntities();

        // GET: AgroPark/MARKs
        public ActionResult Index()
        {
            return View(db.MARKS.ToList());
        }        

        // GET: AgroPark/MARKs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgroPark/MARKs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OBJECTID,NAME")] MARK mARK)
        {
            if (ModelState.IsValid)
            {
                db.MARKS.Add(mARK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mARK);
        }

        // GET: AgroPark/MARKs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARK mARK = db.MARKS.Find(id);
            if (mARK == null)
            {
                return HttpNotFound();
            }
            return View(mARK);
        }

        // POST: AgroPark/MARKs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OBJECTID,NAME")] MARK mARK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mARK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mARK);
        }

        // GET: AgroPark/MARKs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARK mARK = db.MARKS.Find(id);
            if (mARK == null)
            {
                return HttpNotFound();
            }
            return View(mARK);
        }

        // POST: AgroPark/MARKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MARK mARK = db.MARKS.Find(id);
            db.MARKS.Remove(mARK);
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

        public void AddMark(string name)
        {
            MARK new_mark = new MARK
            {
                NAME = name
            };

            db.MARKS.Add(new_mark);
            db.SaveChanges();
        }

        public void EditMark(int id, string name)
        {
            MARK slct_mark = db.MARKS.Find(id);

            slct_mark.NAME = name;
            db.SaveChanges();
        }
    }
}
