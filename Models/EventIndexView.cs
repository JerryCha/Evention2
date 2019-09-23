namespace Evention2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EventIndexView : DbContext
    {
        public EventIndexView()
            : base("name=EventIndexView")
        {
        }

        public virtual DbSet<EventIndex> EventIndexes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
