using Evention2.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using System.Text;

namespace Evention2.Services
{
    public class EmailServices
    {
        // API KEY of SendGrid
        private const string API_KEY = "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";
        private SendGridClient client;

        private Entity dbHelper;
        public EmailServices()
        {
            client = new SendGridClient(API_KEY);
            dbHelper = new Entity();
        }

        /**
         * Sharing event by email service.
         */
        public void ShareByEmail(string emailAddr, int eId)
        {
            Event e = dbHelper.Events.Find(eId);
            if (e != null)
            {
                var content = string.Format("<h1>{0}</h1><p>{1}</p><p>It will be hold from {2} to {3} at {4}</p>",
                                                e.EventName, e.EventDesc, e.Start_date, e.End_date,
                                                e.Street + ", " + e.Surburb + ", " + e.PostCode);

                var to = new EmailAddress(emailAddr);
                var from = new EmailAddress("share@evention2.com");
                var subject = "You are shared an event - " + e.EventName;
                var m = MailHelper.CreateSingleEmail(from, to, subject, content, content);
                m.AddAttachment(e.EventName + ".ics", GenerateICSBase64(e));
                var response = client.SendEmailAsync(m);
            }
        }

        /**
         * Generating Calendar file (.ics)
         */
        private string GenerateICSBase64(Event e)
        {
            var calEvent = new CalendarEvent {
                Start = new CalDateTime(e.Start_date),
                End = new CalDateTime(e.End_date),
                Summary = e.EventName,
                Description = e.EventDesc,
                Location = e.GetAddress()
            };
            var cal = new Calendar();
            cal.Events.Add(calEvent);

            var serialzedCal = new CalendarSerializer().SerializeToString(cal);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serialzedCal));
        }
    }
}