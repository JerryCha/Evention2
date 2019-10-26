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
        /**
         * Only administrator could access and delete.
         */
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
        /**
         * Prohibit the access to a particular rate's detail page.
         */
        public ActionResult Details(int? id)
        {
            // TODO: Reimplement.
            return new HttpStatusCodeResult(403);
        }

        // GET: Rate/Create
        /**
         * Normally, enter from event page.
         */
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
        /**
         * Post review.
         */
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
        /**
         * Currently prohobiting rate modification.
         */
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
            return new HttpStatusCodeResult(403);
        }

        // POST: Rate/Edit/5
        // Currently prohobiting rate modification.
        [Authorize(Roles="Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Event_id,Reviewer_id,Rating_Score,Comments")] Rate rate)
        {
            return new HttpStatusCodeResult(403);
            //if (ModelState.IsValid)
            //{
            //    db.Entry(rate).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(rate);
        }

        // GET: Rate/Delete/5
        [Authorize(Roles="Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
