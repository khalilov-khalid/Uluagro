using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UluagroAspnet.Models;

namespace UluagroAspnet.Areas.AgroPark.Controllers
{
    [AdminAuthenticationController]
    public class GroupRolesController : Controller
    {
        OculusEntities db = new OculusEntities();

        // GET: AgroPark/GroupRoles
        public ActionResult Index()
        {
            ViewBag.groups = db.GROUPs.ToList();
            List<Role> rols = db.Roles.ToList();
            return View(rols);
        }

        public ActionResult Create()
        {
            ViewBag.Action_id = db.Actions.OrderByDescending(x=>x.ID).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(List<Role> rs, string groupname)
        {
            GROUP new_group = new GROUP();
            new_group.Name = groupname;
            db.GROUPs.Add(new_group);
            db.SaveChanges();
                        
            foreach (var item in rs)
            {
                item.Group_id = new_group.ID;
                db.Roles.Add(item);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public ActionResult OneEdit(int id)
        {
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost]
        public ActionResult OneEdit(Role role)
        {
           
            db.Entry(role).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult AllEdit()
        {
            List<Role> allRoles = db.Roles.ToList();
            return View(allRoles);
        }
        [HttpPost]
        public ActionResult AllEdit(List<Role> roles)
        {
            foreach (var item in roles)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


    }
}