using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Proxies;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NetworkManager.Models
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite("Data Source=demo.db");
                

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ports>()
                .HasOne<Switches>(s => s.Switch)
                .WithMany(p => p.ports)
                .HasForeignKey(s => s.switchId);
            modelBuilder.Entity<TaggedVlans>()
                .HasOne<Ports>(p => p.port)
                .WithMany(v => v.taggedVlans)
                .HasForeignKey(p => p.portId);
        }

        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Switches> Switches { get; set; }
        public virtual DbSet<Ports> Ports { get; set; }
        public virtual DbSet<SwitchModels> SwitchModels { get; set; }
        public virtual DbSet<Vlans> Vlans { get; set; }
        public virtual DbSet<TaggedVlans> TaggedVlans {get; set; }
    }   
}
