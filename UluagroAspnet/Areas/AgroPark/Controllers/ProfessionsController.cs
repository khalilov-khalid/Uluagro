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
    public class ProfessionsController : Controller
    {
        private OculusEntities db = new OculusEntities();
        
        // GET: AgroPark/Professions
        public ActionResult Index()
        {
            return View(db.Professions.ToList());
        }

        // GET: AgroPark/Professions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profession profession = db.Professions.Find(id);
            if (profession == null)
            {
                return HttpNotFound();
            }
            return View(profession);
        }

        // GET: AgroPark/Professions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgroPark/Professions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CanResponser")] Profession profession)
        {
            if (ModelState.IsValid)
            {
                db.Professions.Add(profession);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profession);
        }

        // GET: AgroPark/Professions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profession profession = db.Professions.Find(id);
            if (profession == null)
            {
                return HttpNotFound();
            }
            return View(profession);
        }

        // POST: AgroPark/Professions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,CanResponser")] Profession profession)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profession).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profession);
        }

        // GET: AgroPark/Professions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profession profession = db.Professions.Find(id);
            if (profession == null)
            {
                return HttpNotFound();
            }
            return View(profession);
        }

        // POST: AgroPark/Professions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profession profession = db.Professions.Find(id);
            db.Professions.Remove(profession);
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

        public void AddWorkProfession(string name, bool canreponser)
        {
            Profession new_pr = new Profession
            {
                Name = name,
                CanResponser = canreponser
            };

            db.Professions.Add(new_pr);
            db.SaveChanges();
        }

        public void EditWorkProfession(int id, string name, bool canresponser)
        {
            Profession slctd_pr = db.Professions.Find(id);

            slctd_pr.Name = name;
            slctd_pr.CanResponser = canresponser;
            db.SaveChanges();
        }
    }
}
