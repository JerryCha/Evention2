using Evention2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using static Evention2.Models.StatisticsModel;

namespace Evention2.Services
{
    public static class EventVisitLogServices
    {
        private static Entity dbHelper = new Entity();
        public static void LogVisit(int EventId, string IpAddr, string ViewerId = null)
        {
            Thread.Sleep(10000);
            dbHelper.EventVisitLogs.Add(new EventVisitLog(EventId, IpAddr, ViewerId));
        }

        public static int GetEventViewedTimes(int eventId)
        {
            return dbHelper.EventVisitLogs.Where(evl => evl.Event_Id == eventId).Count();
        }
    }
}