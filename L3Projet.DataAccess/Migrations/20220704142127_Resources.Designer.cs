﻿// <auto-generated />
using System;
using L3Projet.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace L3Projet.DataAccess.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20220704142127_Resources")]
    partial class Resources
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("L3Projet.Common.Models.Building", b =>
                {
                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid>("PlanetId")
                        .HasColumnType("uuid");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.HasKey("Type", "PlanetId");

                    b.HasIndex("PlanetId");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("L3Projet.Common.Models.Planet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastCalculation")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Planets");
                });

            modelBuilder.Entity("L3Projet.Common.Models.Resource", b =>
                {
                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid>("PlanetId")
                        .HasColumnType("uuid");

                    b.Property<double>("Quantity")
                        .HasColumnType("double precision");

                    b.HasKey("Type", "PlanetId");

                    b.HasIndex("PlanetId");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("L3Projet.Common.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("L3Projet.Common.Models.Building", b =>
                {
                    b.HasOne("L3Projet.Common.Models.Planet", null)
                        .WithMany("Buildings")
                        .HasForeignKey("PlanetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("L3Projet.Common.Models.Planet", b =>
                {
                    b.HasOne("L3Projet.Common.Models.User", null)
                        .WithMany("Planets")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("L3Projet.Common.Models.Resource", b =>
                {
                    b.HasOne("L3Projet.Common.Models.Planet", null)
                        .WithMany("Resources")
                        .HasForeignKey("PlanetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("L3Projet.Common.Models.Planet", b =>
                {
                    b.Navigation("Buildings");

                    b.Navigation("Resources");
                });

            modelBuilder.Entity("L3Projet.Common.Models.User", b =>
                {
                    b.Navigation("Planets");
                });
#pragma warning restore 612, 618
        }
    }
}
