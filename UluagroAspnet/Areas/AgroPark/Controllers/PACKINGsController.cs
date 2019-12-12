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
    public class PACKINGsController : Controller
    {
        private OculusEntities db = new OculusEntities();

        // GET: AgroPark/PACKINGs
        public ActionResult Index()
        {
            return View(db.PACKINGs.ToList());
        }
        
        // GET: AgroPark/PACKINGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgroPark/PACKINGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OBJECTID,NAME")] PACKING pACKING)
        {
            if (ModelState.IsValid)
            {
                db.PACKINGs.Add(pACKING);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pACKING);
        }

        // GET: AgroPark/PACKINGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACKING pACKING = db.PACKINGs.Find(id);
            if (pACKING == null)
            {
                return HttpNotFound();
            }
            return View(pACKING);
        }

        // POST: AgroPark/PACKINGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OBJECTID,NAME")] PACKING pACKING)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pACKING).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pACKING);
        }

        // GET: AgroPark/PACKINGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACKING pACKING = db.PACKINGs.Find(id);
            if (pACKING == null)
            {
                return HttpNotFound();
            }
            return View(pACKING);
        }

        // POST: AgroPark/PACKINGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PACKING pACKING = db.PACKINGs.Find(id);
            db.PACKINGs.Remove(pACKING);
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

        public void AddPack(string name)
        {
            PACKING new_pack = new PACKING
            {
                NAME = name
            };
            db.PACKINGs.Add(new_pack);
            db.SaveChanges();
        }

        public void EditPack(int id, string name)
        {
            PACKING slctd_p = db.PACKINGs.Find(id);

            slctd_p.NAME = name;
            db.SaveChanges();
        }
    }
}
