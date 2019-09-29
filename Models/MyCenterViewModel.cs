using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Evention2.Models
{
    public class MyIndexViewModel
    {
        public MyIndexViewModel(UserProfile profile, List<Event> events)
        {
            this.Profile = profile;
            this.MyEvents = events;
        }
        public UserProfile Profile { get; set; }
        public List<Event> MyEvents { get; set; }
    }
    public partial class UserProfile
    {
        public string Id { get; set; }

        [StringLength(64)]
        public string FirstName { get; set; }

        [StringLength(64)]
        public string LastName { get; set; }

        [StringLength(64)]
        public string MiddleName { get; set; }

        [StringLength(128)]
        public string Street { get; set; }

        [StringLength(64)]
        public string Surburb { get; set; }

        [StringLength(16)]
        public string State { get; set; }

        [StringLength(16)]
        public string PostCode { get; set; }

        public DateTime? Birthday { get; set; }

        public int? Gender { get; set; }
    }
}