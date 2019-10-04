using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Evention2.Models;
using Microsoft.AspNet.Identity;

namespace Evention2.Controllers
{
    [Authorize]
    public class RateController : Controller
    {
        private Entity db = new Entity();

        // GET: Rate
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            if (Request.QueryString.Count != 0)
            {
                // TODO: Implement conditional query.
                // e.g. Search for an event's rates.
            }
            return View(db.Rates.ToList());
        }

        // GET: Rate/Details/5
        public ActionResult Details(int? id)
        {
            // TODO: Reimplement.
            return new HttpStatusCodeResult(403);
        }

        // GET: Rate/Create
        public ActionResult Create(int? eventId)
        {
            if (eventId == null)
            {
                return new HttpStatusCodeResult(400);
            }
            var e = db.Events.Find(eventId);
            if (e == null)
            {
                return new HttpStatusCodeResult(404);
            }
            RateCreateViewModel r = new RateCreateViewModel();
            r.Event_id = e.EventId;
            r.Event_name = e.EventName;
            return View(r);
        }

        // POST: Rate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int eventId, [Bind(Include = "Rating_Score,Comments")] RateCreateViewModel rate)
        {
            rate.Event_id = eventId;
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                Rate newRate = rate.ToRate();
                newRate.Reviewer_id = userId;
                try
                {
                    db.Rates.Add(newRate);
                    db.SaveChanges();
                    return Redirect("/Event/Details/" + eventId);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return View(rate);
                }
            }

            return View(rate);
        }

        // GET: Rate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }

        // POST: Rate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles="Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Event_id,Reviewer_id,Rating_Score,Comments")] Rate rate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rate);
        }

        // GET: Rate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }

        // POST: Rate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rate rate = db.Rates.Find(id);
            db.Rates.Remove(rate);
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
