using ImmobiliareApi.Entities;
using ImmobiliareApi.Entities.Typologies;
using ImmobiliareApi.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ImmobiliareApi.DataContext
{
    public class ImmobiliareApiContext : IdentityDbContext 
    {
        public ImmobiliareApiContext(DbContextOptions<ImmobiliareApiContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureEntities();

        }
        
    }
}


