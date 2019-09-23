namespace Evention2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int Event_id { get; set; }

        [Required]
        [StringLength(128)]
        public string Reviewer_id { get; set; }

        public int Rating_Score { get; set; }

        [StringLength(64)]
        public string Comments { get; set; }

        public virtual Event Event { get; set; }
    }
}
