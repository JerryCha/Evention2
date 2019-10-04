namespace Evention2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Text;

    public partial class Rate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Event_id { get; set; }

        [Required]
        [StringLength(128)]
        public string Reviewer_id { get; set; }
        [Range(1,5)]
        public int Rating_Score { get; set; }

        [StringLength(64)]
        public string Comments { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Rate ID: " + Id + ", ");
            builder.Append("Event ID: " + Event_id + ", ");
            builder.Append("Reviewer ID: " + Reviewer_id + ", ");
            builder.Append("Rating Score: " + Rating_Score + ", ");
            builder.Append("Comments: " + Comments);
            return builder.ToString();
        }

        public virtual Event Event { get; set; }
    }
}
