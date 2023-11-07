﻿using ByteMasterAPI.Model;
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
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Orcamento>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();
        }

        public DbSet<Cliente> clientetb { get; set; }
        public DbSet<Orcamento> orcamentotb { get; set; }
    }
}
