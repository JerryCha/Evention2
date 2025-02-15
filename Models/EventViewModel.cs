﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Evention2.Models
{

    public class EventDetailViewModel
    {
        public EventDetailViewModel(Event aEvent, List<EventRate> eventRates, List<Ticket> tickets)
        {
            this.aEvent = aEvent;
            this.eventRates = eventRates;
            this.tickets = tickets;
        }
        public Event aEvent { get; set; }
        public List<EventRate> eventRates { get; set; }
        public List<Ticket> tickets { get; set; }
    }

    public class EventCreateViewModel
    {
        public int EventId { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        public string EventDesc { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime Start_date { get; set; }

        [Required]
        public DateTime End_date { get; set; }

        [Required]
        public int EventType { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Surburb { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string PostCode { get; set; }

        public Event ToEvent()
        {
            Event e = new Event();
            e.EventId = this.EventId;
            e.EventName = this.EventName;
            e.EventDesc = this.EventDesc;
            e.Email = this.Email;
            e.Phone = this.Phone;
            e.Start_date = this.Start_date;
            e.End_date = this.End_date;
            e.EventType = this.EventType;
            e.Street = this.Street;
            e.Surburb = this.Surburb;
            e.State = this.State;
            e.PostCode = this.PostCode;
            return e;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("EventId: " + EventId + ", ");
            builder.Append("EventName: " + EventName + ", ");
            builder.Append("EventDesc: " + EventDesc + ", ");
            builder.Append("Phone: " + Phone + ", ");
            builder.Append("Email: " + Email + ", ");
            builder.Append("Address: " + Street + ", " + Surburb + ", " + State + ", " + PostCode);
            builder.Append("Start_date: " + Start_date + ", ");
            builder.Append("End_date: " + End_date + ", ");
            builder.Append("EventType: " + EventType);
            return builder.ToString();
        }
    }
}