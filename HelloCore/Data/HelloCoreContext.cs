using HelloCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCore.Data
{
    public class HelloCoreContext : DbContext
    {
        public HelloCoreContext (DbContextOptions<HelloCoreContext> options) : base(options)
        {
        }

        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Bestelling> Bestellingen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Klant>().ToTable("Klant");
            modelBuilder.Entity<Bestelling>().ToTable("Bestelling").Property(p => p.Prijs).HasColumnType("decimal(18,2)");
        }
    }
}
