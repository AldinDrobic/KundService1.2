namespace KundService1._2
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class KundModell : DbContext
    {
        public KundModell()
            : base("name=KundModell")
        {
        }

        public virtual DbSet<Kund> Kund { get; set; }
        public virtual DbSet<Notis> Notis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
