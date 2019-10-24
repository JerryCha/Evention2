using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evention2.Models
{
    public class TicketOrderView
    {
        public List<Ticket> Tickets { get; set; }
        public string EventName { get; set; }
        public TicketOrderView(string EventName, List<Ticket> Tickets)
        {
            this.EventName = EventName;
            this.Tickets = Tickets;
        }
    }

    public class PlaceOrderModel
    {
        public Order UserOrder { get; set; }
        public List<TicketOrder> Tickets { get; set; }
    }
}