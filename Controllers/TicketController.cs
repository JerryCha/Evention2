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
            var @event = db.Events.Find(id);
            if (@event.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
            TicketOrderView v = new TicketOrderView(@event.EventName, db.Ticket.Where(r => r.Event_Id == id).ToList());
            return View(v);
        }

        public ActionResult Edit(int? id)
        {
            var @event = db.Events.Find(id);
            if (@event.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
            TicketOrderView v = new TicketOrderView(@event.EventName, db.Ticket.Where(r => r.Event_Id == id).ToList());
            return View(v);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include ="EventName,Tickets")]TicketOrderView tov)
        {
            if (ModelState.IsValid)
            {
                foreach (Ticket ticket in tov.Tickets)
                {
                    
                    db.Entry(ticket.Sku_Id).State = System.Data.Entity.EntityState.Modified;
                    return RedirectToAction("Details", "Ticket");
                }
            }
            return View(tov);
        }
        public ActionResult Create(int? id)
        {
            var @event = db.Events.Find(id);
            if (@event.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
            return View(new TicketOrderView(@event.EventName, null));
        }

    }
}