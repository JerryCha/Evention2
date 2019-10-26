namespace Evention2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TicketOrder")]
    public partial class TicketOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Order_Id { get; set; }

        public int Sku_Id { get; set; }

        public int Qty { get; set; }

        public virtual Order Order { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
