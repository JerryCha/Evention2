namespace Evention2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ticket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ticket()
        {
            TicketOrders = new HashSet<TicketOrder>();
        }

        [Key]
        public int Sku_Id { get; set; }

        public int Event_Id { get; set; }

        [Required]
        [StringLength(32)]
        public string Sku_Name { get; set; }

        [Required]
        [StringLength(64)]
        public string Sku_Desc { get; set; }

        public double Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketOrder> TicketOrders { get; set; }
    }
}
