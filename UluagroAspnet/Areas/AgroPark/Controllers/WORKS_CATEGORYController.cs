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
    public class WORKS_CATEGORYController : Controller
    {
        private OculusEntities db = new OculusEntities();

        // GET: AgroPark/WORKS_CATEGORY
        public ActionResult Index()
        {
            return View(db.WORKS_CATEGORY.ToList());
        }        

        // GET: AgroPark/WORKS_CATEGORY/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgroPark/WORKS_CATEGORY/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OBJECTID,NAME")] WORKS_CATEGORY wORKS_CATEGORY)
        {
            if (ModelState.IsValid)
            {
                db.WORKS_CATEGORY.Add(wORKS_CATEGORY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wORKS_CATEGORY);
        }
        // GET: AgroPark/WORKS_CATEGORY/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WORKS_CATEGORY wORKS_CATEGORY = db.WORKS_CATEGORY.Find(id);
            if (wORKS_CATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(wORKS_CATEGORY);
        }

        // POST: AgroPark/WORKS_CATEGORY/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OBJECTID,NAME")] WORKS_CATEGORY wORKS_CATEGORY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wORKS_CATEGORY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wORKS_CATEGORY);
        }

        // GET: AgroPark/WORKS_CATEGORY/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WORKS_CATEGORY wORKS_CATEGORY = db.WORKS_CATEGORY.Find(id);
            if (wORKS_CATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(wORKS_CATEGORY);
        }

        // POST: AgroPark/WORKS_CATEGORY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WORKS_CATEGORY wORKS_CATEGORY = db.WORKS_CATEGORY.Find(id);
            db.WORKS_CATEGORY.Remove(wORKS_CATEGORY);
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

        public void AddWorkCategory(string name)
        {
            WORKS_CATEGORY new_wk = new WORKS_CATEGORY
            {
                NAME = name
            };

            db.WORKS_CATEGORY.Add(new_wk);
            db.SaveChanges();
        }

        public void EditWorkCategory(string name, int id)
        {
            WORKS_CATEGORY slctd_wk = db.WORKS_CATEGORY.Find(id);

            slctd_wk.NAME = name;
            db.SaveChanges();
        }
    }
}
