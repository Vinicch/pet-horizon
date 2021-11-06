using apifmu.Models;
using Microsoft.EntityFrameworkCore;

namespace apifmu.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<Usuario>()
                .HasIndex(u => u.Cpf)
                .IsUnique();

            builder.Entity<Ongs>()
                .HasIndex(u => u.Cnpj)
                .IsUnique();
        }
        public DbSet<Animal> animal { get; set; }
        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Endereco> endereco { get; set; }
        public DbSet<Ongs> ongs { get; set; }
        public DbSet<Pessoa> pessoa { get; set; }
        public DbSet<Usuario> usuario { get; set; }
        public DbSet<UsuarioDados> usuariodados { get; set; }
        public DbSet<Forum> forum { get; set; }
        public DbSet<Adoption> adoption { get; set; }
        public DbSet<ForumResposta> forumresposta { get; set; }

    }
}

// "Default": "Server=databasefmu.c1pazaf62rtn.us-east-2.rds.amazonaws.com;Port=3306;Database=databasefmu;Uid=admin;Pwd=laekle050404;charset=utf8;"