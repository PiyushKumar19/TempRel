﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TempRel.Models;

#nullable disable

namespace TempRel.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240119045754_For TempModel.")]
    partial class ForTempModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TempRel.Models.Model1", b =>
                {
                    b.Property<int>("BaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BaseId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BaseId");

                    b.ToTable("Model1s");
                });

            modelBuilder.Entity("TempRel.Models.Model2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("BaseId")
                        .HasColumnType("int");

                    b.Property<int>("Model1BaseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Model1BaseId");

                    b.ToTable("Model2s");
                });

            modelBuilder.Entity("TempRel.Models.TempModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TokenList")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TempModel");
                });

            modelBuilder.Entity("TempRel.Models.Model2", b =>
                {
                    b.HasOne("TempRel.Models.Model1", "Model1")
                        .WithMany("Model2")
                        .HasForeignKey("Model1BaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model1");
                });

            modelBuilder.Entity("TempRel.Models.Model1", b =>
                {
                    b.Navigation("Model2");
                });
#pragma warning restore 612, 618
        }
    }
}
