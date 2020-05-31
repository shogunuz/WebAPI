﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.Models;

namespace WebAPI.Migrations.WorkerHoliday
{
    [DbContext(typeof(WorkerHolidayContext))]
    partial class WorkerHolidayContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAPI.Models.WorkerHoliday", b =>
                {
                    b.Property<int>("IdForH")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DateEnd")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("DateStart")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("FIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PMId")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("IdForH");

                    b.ToTable("WorkerHolidays");
                });
#pragma warning restore 612, 618
        }
    }
}
