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
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description");

                    b.Property<bool>("isUplink");

                    b.Property<DateTime>("lastUpdate");

                    b.Property<string>("lldpRemoteDeviceName");

                    b.Property<int>("name");

                    b.Property<string>("state");

                    b.Property<int>("switchId");

                    b.Property<int>("vlan");

                    b.HasKey("id");

                    b.HasIndex("switchId");

                    b.ToTable("Ports");
                });

            modelBuilder.Entity("NetworkManager.Models.SwitchModels", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("driver");

                    b.Property<string>("name");

                    b.Property<int>("portCount");

                    b.HasKey("id");

                    b.ToTable("SwitchModels");
                });

            modelBuilder.Entity("NetworkManager.Models.Switches", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ipv4");

                    b.Property<DateTime>("lastUpdate");

                    b.Property<string>("location");

                    b.Property<int>("model");

                    b.Property<string>("modelName");

                    b.Property<string>("name");

                    b.Property<string>("password");

                    b.Property<string>("username");

                    b.HasKey("id");

                    b.ToTable("Switches");
                });

            modelBuilder.Entity("NetworkManager.Models.TaggedVlan", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("portId");

                    b.Property<int>("vlanId");

                    b.HasKey("id");

                    b.HasIndex("portId");

                    b.ToTable("TaggedVlan");
                });

            modelBuilder.Entity("NetworkManager.Models.Vlan", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("name");

                    b.Property<int>("vlanId");

                    b.HasKey("id");

                    b.ToTable("Vlans");
                });

            modelBuilder.Entity("NetworkManager.Models.Ports", b =>
                {
                    b.HasOne("NetworkManager.Models.Switches", "Switch")
                        .WithMany("ports")
                        .HasForeignKey("switchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NetworkManager.Models.TaggedVlan", b =>
                {
                    b.HasOne("NetworkManager.Models.Ports", "port")
                        .WithMany("taggedVlans")
                        .HasForeignKey("portId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
