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
}
