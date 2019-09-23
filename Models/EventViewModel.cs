using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Evention2.Models
{
    [Table("EventIndex", Schema = "aspnet-Evention2-20190903080703")]
    public class EventIndexModel
    {
        [Key]
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDesc { get; set; }
        public string TypeName { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public string PosterImg { get; set; }
    }
}