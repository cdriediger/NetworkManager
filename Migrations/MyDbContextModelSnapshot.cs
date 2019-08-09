﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetworkManager.Models;

namespace NetworkManager.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("NetworkManager.Models.Admins", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Created");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("NetworkManager.Models.Ports", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("PortId");

                    b.Property<int>("SwitchId");

                    b.Property<int>("Vlan");

                    b.HasKey("Id");

                    b.HasIndex("SwitchId");

                    b.ToTable("Ports");
                });

            modelBuilder.Entity("NetworkManager.Models.SwitchModels", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Driver");

                    b.Property<string>("Name");

                    b.Property<int>("PortCount");

                    b.HasKey("Id");

                    b.ToTable("SwitchModels");
                });

            modelBuilder.Entity("NetworkManager.Models.Switches", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IPv4");

                    b.Property<string>("Location");

                    b.Property<int>("Model");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Switches");
                });

            modelBuilder.Entity("NetworkManager.Models.Ports", b =>
                {
                    b.HasOne("NetworkManager.Models.Switches", "Switch")
                        .WithMany("Ports")
                        .HasForeignKey("SwitchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
