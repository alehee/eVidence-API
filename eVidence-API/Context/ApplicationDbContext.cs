﻿using eVidence_API.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace eVidence_API.Context
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Entrance> EntranceHistory { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<ProcessHistory> ProcessesHistory { get; set; }
        public virtual DbSet<TemporaryCard> TemporaryCards { get; set; }
        public virtual DbSet<TemporaryEntrance> TemporaryEntranceHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Environment.DB_CONNECTION_STRING);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(a => a.Departments)
                .WithMany(a => a.Groups)
                .UsingEntity(a => a.ToTable("GroupDepartments"));
        }
    }
}
