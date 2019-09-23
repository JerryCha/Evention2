namespace Evention2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            Rates = new HashSet<Rate>();
        }

        public int EventId { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        public string EventDesc { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime Start_date { get; set; }

        public DateTime End_date { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [Required]
        public string PosterImg { get; set; }

        public int EventType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rate> Rates { get; set; }

        public virtual Type Type { get; set; }
    }
}
