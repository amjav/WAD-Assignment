// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WAD_Assignment.Models;

namespace WAD_Assignment.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210309131949_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WAD_Assignment.Model.Film", b =>
                {
                    b.Property<int>("FilmID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FilmCertificate")
                        .IsRequired()
                        .HasColumnType("varchar(5)");

                    b.Property<string>("FilmDescription")
                        .HasColumnType("text");

                    b.Property<string>("FilmImage")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("FilmPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FilmTitle")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.HasKey("FilmID");

                    b.ToTable("Films");
                });
#pragma warning restore 612, 618
        }
    }
}
