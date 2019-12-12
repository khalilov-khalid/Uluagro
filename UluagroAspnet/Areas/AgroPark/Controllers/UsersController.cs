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
    public class UsersController : Controller
    {
        private OculusEntities db = new OculusEntities();

        // GET: AgroPark/Users
        public ActionResult Index()
        {
            var users = db.Users.Where(s=>s.ID!=1).Include(u => u.GROUP).Include(u => u.USERADMINSTATU).Include(u => u.Worker);
            return View(users.ToList());
        }
        

        // GET: AgroPark/Users/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.GROUPs, "ID", "Name");
            ViewBag.UserAdmin_ID = new SelectList(db.USERADMINSTATUS, "OBJECTID", "NAME");
            ViewBag.WorkerID = new SelectList(db.Workers, "ID", "Name");
            return View();
        }

        // POST: AgroPark/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,WorkerID,Username,Password,GroupID")] User user)
        {
            if (ModelState.IsValid)
            {
                user.CreatedDate = DateTime.Now;
                user.UserAdmin_ID = 2;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.GROUPs, "ID", "Name", user.GroupID);
            ViewBag.UserAdmin_ID = new SelectList(db.USERADMINSTATUS, "OBJECTID", "NAME", user.UserAdmin_ID);
            ViewBag.WorkerID = new SelectList(db.Workers, "ID", "Name", user.WorkerID);
            return View(user);
        }

        // GET: AgroPark/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.GROUPs, "ID", "Name", user.GroupID);
            return View(user);
        }

        // POST: AgroPark/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int ID,int GroupID)
        {
            var user = db.Users.Find(ID);
            if (ModelState.IsValid)
            {                
                user.GroupID = GroupID;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.GROUPs, "ID", "Name", user.GroupID);
            return View();
        }

        // GET: AgroPark/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: AgroPark/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
