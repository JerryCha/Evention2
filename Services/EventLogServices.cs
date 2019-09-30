using Evention2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
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
            Debug.WriteLine(string.Format("EventId: {0}, IP Address: {1}, viewerId: {2}", EventId, IpAddr, ViewerId));
            var newVisitRecord = new EventVisitLog(EventId, IpAddr, ViewerId);
            try
            {
                string insertStmt = string.Format("INSERT INTO EventVisitLog(Viewer_Id, Event_Id, IP_Address, View_Time) VALUES('{0}',{1},'{2}','{3}');",
                                                    ViewerId,
                                                    EventId,
                                                    IpAddr.ToString(),
                                                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Debug.WriteLine(insertStmt);
                dbHelper.Database.ExecuteSqlCommand(insertStmt);
                dbHelper.SaveChanges();
                Debug.WriteLine("Logged");
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Debug.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        public static int GetEventViewedTimes(int eventId)
        {
            return dbHelper.EventVisitLogs.Where(evl => evl.Event_Id == eventId).Count();
        }
    }
}