﻿// <auto-generated />
using Client_EasySound.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Client_EasySound.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("Client_EasySound.Models.client", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<string>("utente")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("eleclient");
                });
#pragma warning restore 612, 618
        }
    }
}