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
using Evention2.Services;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace Evention2.Controllers
{
    public class EventController : Controller
    {
        private Entity db = new Entity();
        private static PosterManagementService posterTempStorage = PosterManagementService.GetInstance();

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
            aEvent.PosterImg = aEvent.PosterImg == "" ? 
                "/Content/img/70634057gy1fw7seyf81mj20bx0blwex.jpg" : "/Content/poster/" + aEvent.PosterImg;

            // Retrieve corresponding rates.
            JoinedViewModel viewModel = new JoinedViewModel();
            List<EventRate> eventRates = viewModel.EventRates.
                                                    Where(r => r.Event_id == id).ToList();
            if (eventRates.Count == 0)
                ViewBag.AvgScore = "No rates";
            else
            {
                double avgScore = 0;
                foreach (var rate in eventRates)
                {
                    avgScore += rate.Rating_Score;
                }
                avgScore /= eventRates.Count();
                ViewBag.AvgScore = String.Format("{0:0.0}", avgScore);
            }
            EventDetailViewModel @event = new EventDetailViewModel(aEvent, eventRates);
            // Apply a new thread to handle visit logging task.
            new Thread(() => {
                    EventVisitLogServices.LogVisit(@event.aEvent.EventId, 
                                                    Request.UserHostAddress.ToString(), 
                                                    User.Identity.GetUserId());
                    }).Start();
            return View(@event);
        }

        // GET: Event/ShareEvent
        // Reference: https://blog.csdn.net/gghh2015/article/details/79116718
        [Authorize]
        [HttpPost]
        public ActionResult ShareEvent()
        {
            string shareEmailAddr = Request.Form["emailAddr"];

            // Email Regex may need to redesign.
            string emailRegex = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            if (new Regex(emailRegex, RegexOptions.IgnoreCase).IsMatch(shareEmailAddr))
            {
                int eventId;
                bool idParseSucceed = Int32.TryParse(Request.Form["eventId"], out eventId);
                if (idParseSucceed)
                {
                    Debug.WriteLine(shareEmailAddr, eventId);
                    var shareHelper = new EmailServices();
                    shareHelper.ShareByEmail(shareEmailAddr, eventId);

                    Response.StatusCode = 200;
                    return Json("success");
                }

                Response.StatusCode = 400;
                return Json("event parse error");
            }
            else
            {
                Response.StatusCode = 400;
                return Json("Email address does not match the pattern");
            }
        }

        // GET: Event/Create
        [Authorize]
        public ActionResult Create()
        {
            List<Type> types = db.Types.ToList();
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
        public ActionResult Create([Bind(Include = "EventType,EventId,EventName,EventDesc,Phone,Email,Start_date,End_date,Street,Surburb,State,PostCode,PosterImage")] EventCreateViewModel @event)
        {
            List<Type> types = db.Types.ToList();
            SelectList selectListItems = new SelectList(types, "TypeId", "TypeName");
            ViewBag.TypeList = selectListItems;
            if (Request.Files.Count == 0)
            {
                return View(@event);
            }

            if (ModelState.IsValid)
            {
                if (@event.Start_date > DateTime.Now && @event.End_date < @event.Start_date)
                {
                    return View(@event);
                }
                var fileLoc = SavePoster(Request.Files[0]);
                Event newEvent = @event.ToEvent();
                var userId = User.Identity.GetUserId();
                newEvent.OwnerId = userId;
                if (fileLoc.Result == null)
                    newEvent.PosterImg = "";
                else
                    newEvent.PosterImg = fileLoc.Result;
                db.Events.Add(newEvent);
                db.SaveChanges();
                return RedirectToAction("Index", "Event");
            }

            return View(@event);
        }


        private async Task<String> SavePoster(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength <= 0)
            {
                return null;
            }

            var fileExt = file.FileName.Split('.').Last().ToLower();
            if (fileExt != "jpg" && fileExt != "png" && fileExt != "webp")
            {
                return null;
            }

            var path = Server.MapPath("~/Content/poster/");
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace.ToString());
                }
            }
            var fileLoc = path + Path.GetFileName(file.FileName);
            file.SaveAs(fileLoc);

            return file.FileName;
        }

        // GET: Event/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            List<Type> types = db.Types.ToList();
            SelectList selectListItems = new SelectList(types, "TypeId", "TypeName");
            ViewBag.TypeList = selectListItems;
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
            if (User.Identity.GetUserId() != @event.OwnerId && !User.IsInRole("Administrator"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(@event.ToCreateViewModel());
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "EventId,EventType,EventName,EventDesc,Phone,Email,Start_date,End_date,Street,Surburb,State,PostCode")] EventCreateViewModel e)
        {
            List<Type> types = db.Types.ToList();
            SelectList selectListItems = new SelectList(types, "TypeId", "TypeName");
            ViewBag.TypeList = selectListItems;

            // Retrieve old data of event
            Event currEvent = db.Events.AsNoTracking().
                                        Where(ee => ee.EventId == e.EventId).
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
            // Date check
            if (e.Start_date > DateTime.Now && e.End_date < e.Start_date)
            {
                return View(e);
            }
            // Update state.
            currEvent.UpdateFromViewModel(e);
            if (ModelState.IsValid)
            {
                db.Entry(currEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(e);
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
