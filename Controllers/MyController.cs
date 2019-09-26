using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Evention2.Models;
using Microsoft.AspNet.Identity;

namespace Evention2.Controllers
{
    public class MyController : Controller
    {
        private Entity db = new Entity();

        // GET: My
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            UserProfile profile = db.UserProfiles.Find(userId);
            return View(profile);
        }

        // GET: My/Details
        public ActionResult Details()
        {
            var userId = User.Identity.GetUserId();
            UserProfile profile = db.UserProfiles.Find(userId);
            return View(profile);
        }

        // GET: My/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: My/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,MiddleName,Street,Surburb,State,PostCode,Birthday,Gender")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userProfile);
        }

        // GET: My/Edit
        public ActionResult Edit()
        {
            UserProfile userProfile = db.UserProfiles.Find(User.Identity.GetUserId());
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // POST: My/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,MiddleName,Street,Surburb,State,PostCode,Birthday,Gender")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userProfile);
        }

        // GET: My/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // POST: My/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserProfile userProfile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userProfile);
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
