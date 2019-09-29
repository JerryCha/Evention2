using Evention2.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evention2.Services
{
    public class EmailServices
    {

        private const string API_KEY = "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";
        private SendGridClient client;

        private Entity dbHelper;
        public EmailServices()
        {
            client = new SendGridClient(API_KEY);
            dbHelper = new Entity();
        }

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
                var response = client.SendEmailAsync(m);
            }
        }
    }
}