using Microsoft.EntityFrameworkCore;
using ProjetoProtech.API.Models;

namespace ProjetoProtech.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Anime> Animes { get; set; } // Garantir que a DbSet está configurada

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aqui você pode adicionar configurações adicionais, como definir o nome da tabela
            modelBuilder.Entity<Anime>().ToTable("Animes");
        }
    }
}
