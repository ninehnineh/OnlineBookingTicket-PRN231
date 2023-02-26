﻿// <auto-generated />
using System;
using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObject.Migrations
{
    [DbContext(typeof(OnlineBookingTicketDbContext))]
    [Migration("20230226060510_FixedDurationTypeInMovieToDatetime")]
    partial class FixedDurationTypeInMovieToDatetime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BusinessObject.Entities.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MovieShowID")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfSeats")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("MovieShowID");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BusinessObject.Entities.Cinema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CityID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("TotalCinemaHalls")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("CityID");

                    b.ToTable("Cinemas");
                });

            modelBuilder.Entity("BusinessObject.Entities.CinemaHall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CinemaID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Totalseats")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("CinemaID");

                    b.ToTable("CinemaHalls");
                });

            modelBuilder.Entity("BusinessObject.Entities.CinemaSeat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CinemaHallID")
                        .HasColumnType("int");

                    b.Property<int>("SeatNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CinemaHallID");

                    b.ToTable("CinemaSeats");
                });

            modelBuilder.Entity("BusinessObject.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("BusinessObject.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Duration")
                        .HasColumnType("datetime");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Language")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Movies", (string)null);
                });

            modelBuilder.Entity("BusinessObject.Entities.MovieShow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CinemaHallID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<DateTime>("Endtime")
                        .HasColumnType("date");

                    b.Property<int>("MovieID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Starttime")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CinemaHallID");

                    b.HasIndex("MovieID");

                    b.ToTable("MovieShows", (string)null);
                });

            modelBuilder.Entity("BusinessObject.Entities.ShowSeat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookingID")
                        .HasColumnType("int");

                    b.Property<int>("CinemaSeatID")
                        .HasColumnType("int");

                    b.Property<int>("MovieShowID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingID");

                    b.HasIndex("CinemaSeatID");

                    b.HasIndex("MovieShowID");

                    b.ToTable("ShowSeats");
                });

            modelBuilder.Entity("BusinessObject.Entities.Booking", b =>
                {
                    b.HasOne("BusinessObject.Entities.MovieShow", "MovieShow")
                        .WithMany("Bookings")
                        .HasForeignKey("MovieShowID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MovieShow");
                });

            modelBuilder.Entity("BusinessObject.Entities.Cinema", b =>
                {
                    b.HasOne("BusinessObject.Entities.City", "City")
                        .WithMany("Cinemas")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("BusinessObject.Entities.CinemaHall", b =>
                {
                    b.HasOne("BusinessObject.Entities.Cinema", "Cinema")
                        .WithMany("CinemaHalls")
                        .HasForeignKey("CinemaID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cinema");
                });

            modelBuilder.Entity("BusinessObject.Entities.CinemaSeat", b =>
                {
                    b.HasOne("BusinessObject.Entities.CinemaHall", "CinemaHall")
                        .WithMany("CinemaSeats")
                        .HasForeignKey("CinemaHallID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CinemaHall");
                });

            modelBuilder.Entity("BusinessObject.Entities.MovieShow", b =>
                {
                    b.HasOne("BusinessObject.Entities.CinemaHall", "CinemaHall")
                        .WithMany("MovieShows")
                        .HasForeignKey("CinemaHallID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BusinessObject.Entities.Movie", "Movie")
                        .WithMany("MovieShows")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CinemaHall");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("BusinessObject.Entities.ShowSeat", b =>
                {
                    b.HasOne("BusinessObject.Entities.Booking", "Booking")
                        .WithMany("ShowSeats")
                        .HasForeignKey("BookingID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BusinessObject.Entities.CinemaSeat", "CinemaSeat")
                        .WithMany("ShowSeats")
                        .HasForeignKey("CinemaSeatID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BusinessObject.Entities.MovieShow", "MovieShow")
                        .WithMany("ShowSeats")
                        .HasForeignKey("MovieShowID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("CinemaSeat");

                    b.Navigation("MovieShow");
                });

            modelBuilder.Entity("BusinessObject.Entities.Booking", b =>
                {
                    b.Navigation("ShowSeats");
                });

            modelBuilder.Entity("BusinessObject.Entities.Cinema", b =>
                {
                    b.Navigation("CinemaHalls");
                });

            modelBuilder.Entity("BusinessObject.Entities.CinemaHall", b =>
                {
                    b.Navigation("CinemaSeats");

                    b.Navigation("MovieShows");
                });

            modelBuilder.Entity("BusinessObject.Entities.CinemaSeat", b =>
                {
                    b.Navigation("ShowSeats");
                });

            modelBuilder.Entity("BusinessObject.Entities.City", b =>
                {
                    b.Navigation("Cinemas");
                });

            modelBuilder.Entity("BusinessObject.Entities.Movie", b =>
                {
                    b.Navigation("MovieShows");
                });

            modelBuilder.Entity("BusinessObject.Entities.MovieShow", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("ShowSeats");
                });
#pragma warning restore 612, 618
        }
    }
}
