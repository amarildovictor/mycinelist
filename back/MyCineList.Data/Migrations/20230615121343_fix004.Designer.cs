﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyCineList.Data.Context;

#nullable disable

namespace MyCineList.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230615121343_fix004")]
    partial class fix004
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.Auth.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.Genre", b =>
                {
                    b.Property<string>("IMDBGenreID")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("IMDBGenreText")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IMDBGenreID");

                    b.ToTable("GENRE", (string)null);
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.GenreMovie", b =>
                {
                    b.Property<string>("GenresIMDBGenreID")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("MovieID")
                        .HasColumnType("int");

                    b.HasKey("GenresIMDBGenreID", "MovieID");

                    b.HasIndex("MovieID");

                    b.ToTable("GENRE_MOVIE", (string)null);
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.ImageMovie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool>("ConsidererToResizingFunction")
                        .HasColumnType("bit");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("ImdbPrimaryImageUrl")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("MediumImageUrl")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("SmallImageUrl")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ImdbPrimaryImageUrl");

                    b.ToTable("IMAGE_MOVIE", (string)null);
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal?>("IMDBAggregateRatting")
                        .HasPrecision(4, 2)
                        .HasColumnType("decimal(4,2)");

                    b.Property<string>("IMDBID")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("IMDBTitleText")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("IMDBTitleTypeID")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("IMDBTitleTypeText")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ImageMovieID")
                        .HasColumnType("int");

                    b.Property<int?>("ReleaseYear")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IMDBID")
                        .IsUnique();

                    b.HasIndex("IMDBTitleText");

                    b.HasIndex("ImageMovieID");

                    b.ToTable("MOVIE", (string)null);
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.MovieDowloadYearControl", b =>
                {
                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<DateTime>("InfoUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ToUpdateNextCall")
                        .HasColumnType("bit");

                    b.HasKey("Year");

                    b.ToTable("MOVIE_DOWNLOAD_YEAR_CONTROL", (string)null);
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.PlotMovie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("IMDBLanguageID")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("IMDBPlainText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovieID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IMDBLanguageID");

                    b.HasIndex("MovieID")
                        .IsUnique();

                    b.ToTable("PLOT_MOVIE", (string)null);
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.PrincipalCastMovie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("IMDBName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("IMDBNameID")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int?>("ImageID")
                        .HasColumnType("int");

                    b.Property<int?>("MovieID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IMDBName");

                    b.HasIndex("ImageID");

                    b.HasIndex("MovieID");

                    b.ToTable("PRINCIPAL_CAST_MOVIE", (string)null);
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.PrincipalCastMovieCharacter", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("IMDBCharacterName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("PrincipalCastMovieID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IMDBCharacterName");

                    b.HasIndex("PrincipalCastMovieID");

                    b.ToTable("PRINCIPAL_CAST_MOVIE_CHARACTER", (string)null);
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.ReleaseDate", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("Day")
                        .HasColumnType("int");

                    b.Property<int?>("Month")
                        .HasColumnType("int");

                    b.Property<int>("MovieID")
                        .HasColumnType("int");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MovieID")
                        .IsUnique();

                    b.ToTable("RELEASE_DATE", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MyCineList.Domain.Entities.Auth.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MyCineList.Domain.Entities.Auth.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyCineList.Domain.Entities.Auth.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MyCineList.Domain.Entities.Auth.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.GenreMovie", b =>
                {
                    b.HasOne("MyCineList.Domain.Entities.Genre", "Genre")
                        .WithMany("GenreMovie")
                        .HasForeignKey("GenresIMDBGenreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyCineList.Domain.Entities.Movie", "Movie")
                        .WithMany("GenresMovie")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.Movie", b =>
                {
                    b.HasOne("MyCineList.Domain.Entities.ImageMovie", "ImageMovie")
                        .WithMany()
                        .HasForeignKey("ImageMovieID");

                    b.Navigation("ImageMovie");
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.PlotMovie", b =>
                {
                    b.HasOne("MyCineList.Domain.Entities.Movie", "Movie")
                        .WithOne("Plot")
                        .HasForeignKey("MyCineList.Domain.Entities.PlotMovie", "MovieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.PrincipalCastMovie", b =>
                {
                    b.HasOne("MyCineList.Domain.Entities.ImageMovie", "Image")
                        .WithMany()
                        .HasForeignKey("ImageID");

                    b.HasOne("MyCineList.Domain.Entities.Movie", "Movie")
                        .WithMany("PrincipalCastMovies")
                        .HasForeignKey("MovieID");

                    b.Navigation("Image");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.PrincipalCastMovieCharacter", b =>
                {
                    b.HasOne("MyCineList.Domain.Entities.PrincipalCastMovie", "PrincipalCastMovie")
                        .WithMany("PrincipalCastMovieCharacters")
                        .HasForeignKey("PrincipalCastMovieID");

                    b.Navigation("PrincipalCastMovie");
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.ReleaseDate", b =>
                {
                    b.HasOne("MyCineList.Domain.Entities.Movie", "Movie")
                        .WithOne("ReleaseDate")
                        .HasForeignKey("MyCineList.Domain.Entities.ReleaseDate", "MovieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.Genre", b =>
                {
                    b.Navigation("GenreMovie");
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.Movie", b =>
                {
                    b.Navigation("GenresMovie");

                    b.Navigation("Plot");

                    b.Navigation("PrincipalCastMovies");

                    b.Navigation("ReleaseDate");
                });

            modelBuilder.Entity("MyCineList.Domain.Entities.PrincipalCastMovie", b =>
                {
                    b.Navigation("PrincipalCastMovieCharacters");
                });
#pragma warning restore 612, 618
        }
    }
}