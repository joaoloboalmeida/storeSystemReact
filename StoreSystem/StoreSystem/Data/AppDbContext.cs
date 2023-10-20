using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StoreSystem.Data.Mappings;
using StoreSystem.Models;

namespace StoreSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            options.UseSqlServer(configuration.GetConnectionString("Homologacao"));
        }
          

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ClienteMapping());
            builder.ApplyConfiguration(new ProdutoMapping());

            builder.Entity<Cliente>().HasData(new Cliente("João", "04580393147") { Id = 1 });
            builder.Entity<Produto>().HasData(new Produto("Computador", 4000m) { Id = 1 });
        }
    }
}
