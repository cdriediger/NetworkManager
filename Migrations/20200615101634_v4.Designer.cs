﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetworkManager.Models;

namespace NetworkManager.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20200615101634_v4")]
    partial class v4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4");

            modelBuilder.Entity("NetworkManager.Models.Admins", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("NetworkManager.Models.Ports", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isUplink")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("lastUpdate")
                        .HasColumnType("TEXT");

                    b.Property<string>("lldpRemoteDeviceName")
                        .HasColumnType("TEXT");

                    b.Property<int>("name")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("profileid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("state")
                        .HasColumnType("TEXT");

                    b.Property<int>("switchId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("vlan")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("profileid");

                    b.HasIndex("switchId");

                    b.ToTable("Ports");
                });

            modelBuilder.Entity("NetworkManager.Models.Profile", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<int>("nativeVlan")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("NetworkManager.Models.SwitchModels", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("adapter")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<int>("portCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("SwitchModels");
                });

            modelBuilder.Entity("NetworkManager.Models.Switches", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("adaptername")
                        .HasColumnType("TEXT");

                    b.Property<string>("ipv4")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("lastUpdate")
                        .HasColumnType("TEXT");

                    b.Property<string>("location")
                        .HasColumnType("TEXT");

                    b.Property<int>("model")
                        .HasColumnType("INTEGER");

                    b.Property<string>("modelName")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .HasColumnType("TEXT");

                    b.Property<string>("username")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Switches");
                });

            modelBuilder.Entity("NetworkManager.Models.TaggedVlan", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("portId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("profileId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("vlanId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("portId");

                    b.HasIndex("profileId");

                    b.ToTable("TaggedVlans");
                });

            modelBuilder.Entity("NetworkManager.Models.Vlan", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<int>("vlanId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Vlans");
                });

            modelBuilder.Entity("NetworkManager.Models.Ports", b =>
                {
                    b.HasOne("NetworkManager.Models.Profile", "profile")
                        .WithMany()
                        .HasForeignKey("profileid");

                    b.HasOne("NetworkManager.Models.Switches", "Switch")
                        .WithMany("ports")
                        .HasForeignKey("switchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NetworkManager.Models.TaggedVlan", b =>
                {
                    b.HasOne("NetworkManager.Models.Ports", "port")
                        .WithMany("taggedVlans")
                        .HasForeignKey("portId");

                    b.HasOne("NetworkManager.Models.Profile", "profile")
                        .WithMany("taggedVlans")
                        .HasForeignKey("profileId");
                });
#pragma warning restore 612, 618
        }
    }
}
