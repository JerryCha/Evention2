namespace Evention2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public partial class JoinedViewModel : DbContext
    {
        public JoinedViewModel()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<EventRate> EventRates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventRate>()
                .Property(e => e.Comments)
                .IsFixedLength();
        }
    }

    /**
     * Model for View EventRates
     */
    public partial class EventRate
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Event_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(256)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Rating_Score { get; set; }

        [StringLength(64)]
        public string Comments { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Id: " + Id + ", ");
            builder.Append("Event_id: " + Event_id + ", ");
            builder.Append("UserName: " + UserName + ", ");
            builder.Append("Rating_Score: " + Rating_Score + ", ");
            builder.Append("Comments: " + Comments);
            return builder.ToString();
        }
    }
}
