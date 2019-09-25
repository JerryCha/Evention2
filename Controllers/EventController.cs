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
using Type = Evention2.Models.Type;

namespace Evention2.Controllers
{
    public class EventController : Controller
    {
        private Entity db = new Entity();


        // GET: Event
        public ActionResult Index()
        {
            
            return View(db.Events.ToList());
        }

        // GET: Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Retrieve event
            Event aEvent = db.Events.Find(id);
            // Check whether there exists such an event with given id.
            if (aEvent == null)
            {
                return HttpNotFound();
            }
            // Retrieve corresponding rates.
            JoinedViewModel viewModel = new JoinedViewModel();
            List<EventRate> eventRates = viewModel.EventRates.Where(r => r.Event_id == id).ToList();
            double avgScore = 0;
            foreach (var rate in eventRates)
            {
                avgScore += rate.Rating_Score;
            }
            avgScore /= eventRates.Count();
            ViewBag.AvgScore = String.Format("{0:0.0}", avgScore);
            EventDetailViewModel @event = new EventDetailViewModel(aEvent, eventRates);
            return View(@event);
        }

        // GET: Event/Create
        [Authorize]
        public ActionResult Create()
        {
            Entity entityHelper = new Entity();
            List<Type> types = entityHelper.Types.ToList();
            SelectList selectListItems = new SelectList(types, "TypeId", "TypeName");
            ViewBag.TypeList = selectListItems;
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "EventType,EventId,EventName,EventDesc,Phone,Email,Start_date,End_date,PosterImg")] EventCreateViewModel @event)
        {
            Debug.WriteLine(@event);
            Entity entityHelper = new Entity();
            List<Type> types = entityHelper.Types.ToList();
            SelectList selectListItems = new SelectList(types, "TypeId", "TypeName");
            ViewBag.TypeList = selectListItems;
            if (ModelState.IsValid)
            {
                Event newEvent = @event.ToEvent();
                newEvent.OwnerId = User.Identity.GetUserId();
                newEvent.PosterImg = "...";
                Debug.WriteLine(newEvent);
                db.Events.Add(newEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: Event/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (User.Identity.IsAuthenticated == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId() != @event.OwnerId || User.IsInRole("Administrator"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "EventId,EventName,EventDesc,Phone,Email,Start_date,End_date,PosterImg")] Event @event)
        {
            // Retrieve old data of event
            Event currEvent = db.Events.AsNoTracking().
                                        Where(e => e.EventId == @event.EventId).
                                        First();
            // currEvent null if query event not found
            if (currEvent == null)
            {
                return HttpNotFound();
            }
            // Identity check. Guest, member except event owner could not access.
            if (User.Identity.IsAuthenticated == false || 
                (User.Identity.GetUserId() != currEvent.OwnerId && 
                User.IsInRole("Administrator") == false))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            // Assign eventId to new state.
            @event.OwnerId = currEvent.OwnerId;
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Event/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
