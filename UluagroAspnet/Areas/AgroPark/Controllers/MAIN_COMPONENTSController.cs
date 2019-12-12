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
    public class MAIN_COMPONENTSController : Controller
    {
        private OculusEntities db = new OculusEntities();

        // GET: AgroPark/MAIN_COMPONENTS
        public ActionResult Index()
        {
            return View(db.MAIN_COMPONENTS.ToList());
        }

        
        // GET: AgroPark/MAIN_COMPONENTS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgroPark/MAIN_COMPONENTS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OBJECTID,NAME")] MAIN_COMPONENTS mAIN_COMPONENTS)
        {
            if (ModelState.IsValid)
            {
                db.MAIN_COMPONENTS.Add(mAIN_COMPONENTS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mAIN_COMPONENTS);
        }

        // GET: AgroPark/MAIN_COMPONENTS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MAIN_COMPONENTS mAIN_COMPONENTS = db.MAIN_COMPONENTS.Find(id);
            if (mAIN_COMPONENTS == null)
            {
                return HttpNotFound();
            }
            return View(mAIN_COMPONENTS);
        }

        // POST: AgroPark/MAIN_COMPONENTS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OBJECTID,NAME")] MAIN_COMPONENTS mAIN_COMPONENTS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mAIN_COMPONENTS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mAIN_COMPONENTS);
        }

        // GET: AgroPark/MAIN_COMPONENTS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MAIN_COMPONENTS mAIN_COMPONENTS = db.MAIN_COMPONENTS.Find(id);
            if (mAIN_COMPONENTS == null)
            {
                return HttpNotFound();
            }
            return View(mAIN_COMPONENTS);
        }

        // POST: AgroPark/MAIN_COMPONENTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MAIN_COMPONENTS mAIN_COMPONENTS = db.MAIN_COMPONENTS.Find(id);
            db.MAIN_COMPONENTS.Remove(mAIN_COMPONENTS);
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

        public void AddComponent(string name)
        {
            MAIN_COMPONENTS new_com = new MAIN_COMPONENTS
            {
                NAME = name
            };
            db.MAIN_COMPONENTS.Add(new_com);
            db.SaveChanges();
        }

        public void EditComponent(int id, string name)
        {
            MAIN_COMPONENTS sl_com = db.MAIN_COMPONENTS.Find(id);

            sl_com.NAME = name;
            db.SaveChanges();
        }
    }
}
