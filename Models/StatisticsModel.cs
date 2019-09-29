namespace Evention2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public partial class StatisticsModel
    {

        [Table("EventVisitLog")]
        public partial class EventVisitLog
        {
            public EventVisitLog()
            {

            }

            public EventVisitLog(int eventId, string ipAddr, string viewerId = null)
            {
                this.Event_Id = eventId;
                this.IP_Address = ipAddr;
                this.View_Time = new DateTime();
            }

            [Key]
            public string Record_Id { get; set; }

            [StringLength(128)]
            public string Viewer_Id { get; set; }

            public int Event_Id { get; set; }

            [Required]
            [StringLength(15)]
            public string IP_Address { get; set; }

            public DateTime View_Time { get; set; }
        }
    }
}
