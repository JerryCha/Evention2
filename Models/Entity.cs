namespace Evention2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Entity : DbContext
    {
        public Entity()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Rates)
                .WithRequired(e => e.Event)
                .HasForeignKey(e => e.Event_id);

            modelBuilder.Entity<Rate>()
                .Property(e => e.Comments)
                .IsFixedLength();

            modelBuilder.Entity<Type>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.EventType)
                .WillCascadeOnDelete(false);
        }
    }
}
