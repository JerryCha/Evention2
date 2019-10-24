namespace Evention2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [Key]
        public int Order_Id { get; set; }

        [Required]
        [StringLength(128)]
        public string User_Id { get; set; }

        [Required]
        [StringLength(16)]
        public string Pay_Method { get; set; }

        public double Total_Price { get; set; }
    }
}
