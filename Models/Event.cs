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
        
        [Required]
        public int EventType { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Surburb { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string PostCode { get; set; }

        public string GetAddress()
        {
            return Street + ", " + Surburb + ", " + State + " " + PostCode;
        }

        /**
         * Overriden of ToString
         */
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("EventId: " + EventId + ", ");
            builder.Append("EventName: " + EventName + ", ");
            builder.Append("EventDesc: " + EventDesc + ", ");
            builder.Append("Phone: " + Phone + ", ");
            builder.Append("Email: " + Email + ", ");
            builder.Append("Address: " + Street + ", " + Surburb + ", " + State + ", " + PostCode);
            builder.Append("Start_date: " + Start_date + ", ");
            builder.Append("End_date: " + End_date + ", ");
            builder.Append("OwnerId: " + OwnerId + ", ");
            builder.Append("PosterImg: " + PosterImg + ", ");
            builder.Append("EventType: " + EventType);
            return builder.ToString();
        }

        /**
         * Convert to EventCreateViewModel
         */
        public void UpdateFromViewModel(EventCreateViewModel viewModel)
        {
            this.EventName = viewModel.EventName;
            this.EventType = viewModel.EventType;
            this.EventDesc = viewModel.EventDesc;
            this.Phone = viewModel.Phone;
            this.Street = viewModel.Street;
            this.Surburb = viewModel.Surburb;
            this.State = viewModel.State;
            this.Email = viewModel.Email;
            this.PostCode = viewModel.PostCode;
            this.Start_date = viewModel.Start_date;
            this.End_date = viewModel.End_date;
        }

        // Convert to EventCreateViewModel
        public EventCreateViewModel ToCreateViewModel()
        {
            EventCreateViewModel viewModel = new EventCreateViewModel();
            viewModel.EventId = this.EventId;
            viewModel.Email = this.Email;
            viewModel.EventDesc = this.EventDesc;
            viewModel.EventName = this.EventName;
            viewModel.EventType = this.EventType;
            viewModel.Phone = this.Phone;
            viewModel.Start_date = this.Start_date;
            viewModel.End_date = this.End_date;
            viewModel.Street = this.Street;
            viewModel.Surburb = this.Surburb;
            viewModel.State = this.State;
            viewModel.PostCode = this.PostCode;
            return viewModel;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rate> Rates { get; set; }

        public virtual Type Type { get; set; }
    }
}
