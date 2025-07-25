﻿// <auto-generated />
using System;
using CrudPractice1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CrudPractice1.Migrations
{
    [DbContext(typeof(CrudPractice1Context))]
    [Migration("20250513065433_SeedLanguages")]
    partial class SeedLanguages
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CrudPractice1.Models.Language", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            Code = "zh",
                            Name = "中文"
                        },
                        new
                        {
                            Code = "en",
                            Name = "英文"
                        },
                        new
                        {
                            Code = "jp",
                            Name = "日文"
                        });
                });

            modelBuilder.Entity("CrudPractice1.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Awards")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("BoxOffice")
                        .HasColumnType("bigint");

                    b.Property<long>("Budget")
                        .HasColumnType("bigint");

                    b.Property<string>("Cast")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DurationMinutes")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ImdbScore")
                        .HasColumnType("float");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginalTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PosterUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductionCompany")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Synopsis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrailerUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("CrudPractice1.Models.Movie2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Awards")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("BoxOffice")
                        .HasColumnType("bigint");

                    b.Property<long>("Budget")
                        .HasColumnType("bigint");

                    b.Property<string>("Cast")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DurationMinutes")
                        .HasColumnType("int");

                    b.Property<int?>("Genre")
                        .HasColumnType("int");

                    b.Property<double>("ImdbScore")
                        .HasColumnType("float");

                    b.Property<string>("OriginalTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PosterUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductionCompany")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Synopsis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrailerUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movie2");
                });

            modelBuilder.Entity("CrudPractice1.Models.MovieLanguage", b =>
                {
                    b.Property<int>("Movie2Id")
                        .HasColumnType("int");

                    b.Property<string>("LanguageCode")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Movie2Id", "LanguageCode");

                    b.HasIndex("LanguageCode");

                    b.ToTable("MovieLanguages");
                });

            modelBuilder.Entity("CrudPractice1.Models.MovieLanguage", b =>
                {
                    b.HasOne("CrudPractice1.Models.Language", "Language")
                        .WithMany("MovieLanguages")
                        .HasForeignKey("LanguageCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CrudPractice1.Models.Movie2", "Movie2")
                        .WithMany("MovieLanguages")
                        .HasForeignKey("Movie2Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Movie2");
                });

            modelBuilder.Entity("CrudPractice1.Models.Language", b =>
                {
                    b.Navigation("MovieLanguages");
                });

            modelBuilder.Entity("CrudPractice1.Models.Movie2", b =>
                {
                    b.Navigation("MovieLanguages");
                });
#pragma warning restore 612, 618
        }
    }
}
