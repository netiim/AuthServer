using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AuthServer.Models;

namespace AuthServer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Medico>().ToTable("Medicos");
            modelBuilder.Entity<Paciente>().ToTable("Pacientes");

            modelBuilder.Entity<Medico>()
                .HasOne(m => m.User)
                .WithOne()
                .HasForeignKey<Medico>(m => m.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Paciente>(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
