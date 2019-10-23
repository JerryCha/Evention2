using Evention2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evention2.Controllers
{
    public class HomeController : Controller
    {
        private Entity db = new Entity();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        [Authorize(Roles ="Administrator")]
        public ActionResult Dashboard()
        {
            var pvCount = db.EventVisitLogs.GroupBy(x => x.View_Time.Date).Select(x => new { Date = x.Key, Times = x.Count() });
            Debug.WriteLine(pvCount);
            return View();
        }
    }
}