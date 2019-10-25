using Evention2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Evention2.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private static Entity db = new Entity();

        public ActionResult Serial()
        {
            return Json(new { data = db.Ticket.ToList() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Buy(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var @event = db.Events.Find(id);
            List<Ticket> tickets = db.Ticket.Where(r => r.Event_Id == id).ToList();
            return View(new TicketOrderView(@event.EventName, tickets));
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var e = db.Events.Find(id);
            if (e == null)
                new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            ViewBag.event_id = id;
            return View();
        }

        public ActionResult Get(int? id)
        {
            if (id != null && id > 0)
            {
                var @event = db.Events.Find(id);
                if (@event.OwnerId != User.Identity.GetUserId())
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                TicketOrderView v = new TicketOrderView(@event.EventName, db.Ticket.Where(r => r.Event_Id == id).ToList());
                return Json(new { name = v.EventName, tickets = v.Tickets }, JsonRequestBehavior.AllowGet);
            }
            return Json("failed");
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Sku_Id,Sku_Name,Sku_Desc,Event_Id,Price")]Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json("ok");
            }
            return Json(ticket);
        }
        public ActionResult Create(int? id)
        {
            var @event = db.Events.Find(id);
            if (@event.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
            return View(new TicketOrderView(@event.EventName, null));
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Sku_Id,Sku_Name,Sku_Desc,Event_Id,Price")]Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Ticket.Add(ticket);
                db.SaveChanges();
                return Json(ticket);
            }
            return Json("failed");  // TODO: Change proper response.
        }

        [ActionName("Delete")]
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null) 
            {
                Response.StatusCode = 400;   //System.Net.HttpStatusCode.BadRequest
                return Json(new { status = "Missing sku id." });
            }
            Ticket t = db.Ticket.Find(id);
            if (t == null)
            {
                Response.StatusCode = 404;   //System.Net.HttpStatusCode.NotFound
                return Json(new { status = "Not found sku." });
            }
            db.Ticket.Remove(t);
            db.SaveChanges();
            return Json(new { tickets = db.Ticket.Where(r => r.Event_Id == t.Event_Id).ToList() }, JsonRequestBehavior.DenyGet);
        }

    }
}