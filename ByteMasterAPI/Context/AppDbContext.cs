using ByteMasterAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ByteMasterAPI.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.Documento);

            modelBuilder.Entity<Cliente>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Orcamento>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Produto>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }

        public DbSet<Cliente> clientetb { get; set; }
        public DbSet<Orcamento> orcamentotb { get; set; }
        public DbSet<OrdemServico> ostb { get; set; }
        public DbSet<Produto> produtotb { get; set; }
        public DbSet<Situacao> situacaotb { get; set; }
    }
}
