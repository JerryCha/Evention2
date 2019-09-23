namespace Evention2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Text;

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

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("EventId: " + EventId + ", ");
            builder.Append("EventName: " + EventName + ", ");
            builder.Append("EventDesc: " + EventDesc + ", ");
            builder.Append("Phone: " + Phone + ", ");
            builder.Append("Email: " + Email + ", ");
            builder.Append("Start_date: " + Start_date + ", ");
            builder.Append("End_date: " + End_date + ", ");
            builder.Append("OwnerId: " + OwnerId + ", ");
            builder.Append("PosterImg: " + PosterImg + ", ");
            builder.Append("EventType: " + EventType);
            return builder.ToString();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rate> Rates { get; set; }

        public virtual Type Type { get; set; }
    }
}
