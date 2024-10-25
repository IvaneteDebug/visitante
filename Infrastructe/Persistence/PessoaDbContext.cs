using Dev.visitante.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dev.visitante.Infrastructe.Persitence
{
    public class PessoaDbContext : DbContext
    {
        public DbSet<Pessoa>? Pessoas { get; set; }

        public PessoaDbContext(DbContextOptions<PessoaDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>().HasKey(x => x.Id);
        }
    }
}
