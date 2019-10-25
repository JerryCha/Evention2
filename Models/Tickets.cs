namespace Evention2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ticket
    {
        [Key]
        public int Sku_Id { get; set; }

        public int Event_Id { get; set; }

        [Required]
        [StringLength(32)]
        public string Sku_Name { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "Price value must be greater than or equal to zero.")]
        public double Price { get; set; }

        [Required]
        [StringLength(64)]
        public string Sku_Desc { get; set; }
    }

    public class TicketPurchaseModel
    {
        public int Sku_Id { get; set; }

        public int Event_Id { get; set; }

        [StringLength(64)]
        public string Sku_Desc { get; set; }

        [StringLength(32)]
        public string Sku_Name { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "Price value must be greater than or equal to zero.")]
        public double Price { get; set; }

        public int Qty { get; set; }

        public TicketPurchaseModel(Ticket t)
        {
            this.Sku_Id = t.Sku_Id;
            this.Sku_Name = t.Sku_Name;
            this.Sku_Desc = t.Sku_Desc;
            this.Event_Id = t.Event_Id;
            this.Price = t.Price;
            this.Qty = 0;
        }
    }
}
