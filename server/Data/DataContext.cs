using apifmu.Models;
using Microsoft.EntityFrameworkCore;

namespace apifmu.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Pet> Pet { get; set; }

        public DbSet<Ong> Ong { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Adoption> Adoption { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<User>()
                .HasIndex(u => u.Uin)
                .IsUnique();

            builder.Entity<Ong>()
                .HasIndex(u => u.Uin)
                .IsUnique();
        }
    }
}
