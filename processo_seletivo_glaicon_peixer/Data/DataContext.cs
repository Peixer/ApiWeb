using Microsoft.EntityFrameworkCore;
using processo_seletivo_glaicon_peixer.Model;

namespace processo_seletivo_glaicon_peixer.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Usuario> Candidatos { get; set; }

        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .ToTable("Usuario");

            modelBuilder.Entity<Usuario>()
                .Property(s => s.Nome)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(s => s.Email)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .HasKey(x => x.Guid);
        }
    }
}