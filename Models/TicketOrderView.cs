using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Evention2.Models
{
    public class TicketOrderView
    {
        public List<TicketPurchaseModel> Tickets { get; set; }

        public string EventName { get; set; }

        [Required]
        public string Pay_Method { get; set; }
        
        public TicketOrderView(string EventName, List<TicketPurchaseModel> Tickets)
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