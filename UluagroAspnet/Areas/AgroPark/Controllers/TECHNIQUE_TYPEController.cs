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
    public class TECHNIQUE_TYPEController : Controller
    {
        private OculusEntities db = new OculusEntities();

        // GET: AgroPark/TECHNIQUE_TYPE
        public ActionResult Index()
        {
            return View(db.TECHNIQUE_TYPE.ToList());
        }

       
        // GET: AgroPark/TECHNIQUE_TYPE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgroPark/TECHNIQUE_TYPE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OBJECTID,NAME")] TECHNIQUE_TYPE tECHNIQUE_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.TECHNIQUE_TYPE.Add(tECHNIQUE_TYPE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tECHNIQUE_TYPE);
        }

        // GET: AgroPark/TECHNIQUE_TYPE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TECHNIQUE_TYPE tECHNIQUE_TYPE = db.TECHNIQUE_TYPE.Find(id);
            if (tECHNIQUE_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(tECHNIQUE_TYPE);
        }

        // POST: AgroPark/TECHNIQUE_TYPE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OBJECTID,NAME")] TECHNIQUE_TYPE tECHNIQUE_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tECHNIQUE_TYPE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tECHNIQUE_TYPE);
        }

        // GET: AgroPark/TECHNIQUE_TYPE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TECHNIQUE_TYPE tECHNIQUE_TYPE = db.TECHNIQUE_TYPE.Find(id);
            if (tECHNIQUE_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(tECHNIQUE_TYPE);
        }

        // POST: AgroPark/TECHNIQUE_TYPE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TECHNIQUE_TYPE tECHNIQUE_TYPE = db.TECHNIQUE_TYPE.Find(id);
            db.TECHNIQUE_TYPE.Remove(tECHNIQUE_TYPE);
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

        public void AddType(string name)
        {
            TECHNIQUE_TYPE new_type = new TECHNIQUE_TYPE
            {
                NAME = name
            };

            db.TECHNIQUE_TYPE.Add(new_type);
            db.SaveChanges();
        }

        public void EditType(int id, string name)
        {
            TECHNIQUE_TYPE slctd_t = db.TECHNIQUE_TYPE.Find(id);

            slctd_t.NAME = name;
            db.SaveChanges();
        }
    }
}
