using ASP_SIMPLE_CRUD.DbContexts;
using ASP_SIMPLE_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_SIMPLE_CRUD.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD

        readonly UserContext db = new UserContext();
        public ActionResult Index()
        {
            var users = db.Users.ToList();

            return View(users);
        }

        //======================= GET DETAILS
        public ActionResult Details(int id)
        {
            var User = db.Users.FirstOrDefault(x => x.id == id);

            return View(User);
        }

        //======================= DELETE
        public ActionResult Delete(int id)
        {
            var User = db.Users.FirstOrDefault(x => x.id == id);
            if (User == null)
            {
                return new HttpStatusCodeResult(404);
            }
            else
            {
                db.Users.Remove(User);
                db.SaveChanges();
                return RedirectToAction("Index", "CRUD");
            }
        }

        //======================= CREATE NEW USER
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            else
            {
                return new HttpStatusCodeResult(400);
            }
            return RedirectToAction("Index","CRUD");
        }

        //======================= EDIT USER
        public ActionResult Edit(int id)
        {
            var user = db.Users.FirstOrDefault(x => x.id == id);

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var userself = db.Users.FirstOrDefault(x => x.id == user.id);

                if (userself == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }

                userself.Name = user.Name;
                userself.Surname = user.Surname;
                userself.Age = user.Age;

                // ALSO CAN BE WRITTEN AS
                //db.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);
                //db.Entry(user).State = EntityState.Modified;

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        //============================= CLOSE CONNECTION
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