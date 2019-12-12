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
    public class WorkersController : Controller
    {
        private OculusEntities db = new OculusEntities();
        
        // GET: AgroPark/Workers
        public ActionResult Index()
        {
            var workers = db.Workers.Include(w => w.Gender).Include(w => w.Profession);
            return View(workers.ToList());
        }

        // GET: AgroPark/Workers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Worker worker = db.Workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            return View(worker);
        }

        // GET: AgroPark/Workers/Create
        public ActionResult Create()
        {
            //ViewBag.Gerder_id = new SelectList(db.Genders, "ID", "Name");
            ViewBag.Gerder_id = db.Genders.ToList();
            ViewBag.Professions_id = db.Professions.ToList();
            return View();
        }

        // POST: AgroPark/Workers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Professions_id,Name,Surname,Fathername,Gerder_id,Email,Salary,Phone,Fin_number,Serial_number,Birthdate,TurniketID")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                worker.Status = true;
                worker.Create_data = DateTime.Now;
                db.Workers.Add(worker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Gerder_id = new SelectList(db.Genders, "ID", "Name", worker.Gerder_id);
            ViewBag.Professions_id = new SelectList(db.Professions, "ID", "Name", worker.Professions_id);
            return View(worker);
        }

        // GET: AgroPark/Workers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Worker worker = db.Workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            ViewBag.Gerder_id = new SelectList(db.Genders, "ID", "Name", worker.Gerder_id);
            ViewBag.Professions_id = new SelectList(db.Professions, "ID", "Name", worker.Professions_id);
            return View(worker);
        }

        // POST: AgroPark/Workers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Professions_id,Name,Surname,Fathername,Gerder_id,Email,Status,Salary,Create_data,Phone,Fin_number,Serial_number,Birthdate,TurniketID")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(worker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Gerder_id = new SelectList(db.Genders, "ID", "Name", worker.Gerder_id);
            ViewBag.Professions_id = new SelectList(db.Professions, "ID", "Name", worker.Professions_id);
            return View(worker);
        }

        // GET: AgroPark/Workers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Worker worker = db.Workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            return View(worker);
        }

        // POST: AgroPark/Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Worker worker = db.Workers.Find(id);
            db.Workers.Remove(worker);
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
