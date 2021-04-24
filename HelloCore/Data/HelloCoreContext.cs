using HelloCore.Areas.Identity.Data;
using HelloCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCore.Data
{
    public class HelloCoreContext : IdentityDbContext<CustomUser>

    {
        public HelloCoreContext (DbContextOptions<HelloCoreContext> options) : base(options)
        {
        }

        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Bestelling> Bestellingen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Klant>().ToTable("Klant");
            modelBuilder.Entity<Klant>().Property(p => p.Naam).IsRequired();
            modelBuilder.Entity<Klant>().Ignore(p => p.VolledigeNaam);

            modelBuilder.Entity<Bestelling>().ToTable("Bestelling");
            modelBuilder.Entity<Bestelling>().Property(p => p.Prijs).HasColumnType("decimal(18,2)");
        }
    }
}
