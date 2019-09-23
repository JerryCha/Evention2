using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evention2.Models
{
    public class EventDetailViewModel
    {
        public EventDetailViewModel(Event aEvent, List<EventRate> eventRates)
        {
            this.aEvent = aEvent;
            this.eventRates = eventRates;
        }
        public Event aEvent { get; set; }
        public List<EventRate> eventRates { get; set; }
    }
}