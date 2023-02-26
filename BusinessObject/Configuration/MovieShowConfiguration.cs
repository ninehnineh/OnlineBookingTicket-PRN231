using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Configuration
{
    public class MovieShowConfiguration : IEntityTypeConfiguration<MovieShow>
    {
        public void Configure(EntityTypeBuilder<MovieShow> builder)
        {
            builder.ToTable("MovieShows")
                .HasKey(x => x.Id);

            builder.Property(x => x.Date)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.Starttime)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.Endtime)
                .HasColumnType("date")
                .IsRequired();

            builder.HasOne(x => x.Movie)
                .WithMany(x => x.MovieShows)
                .HasForeignKey(x => x.MovieID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CinemaHall)
                .WithMany(x => x.MovieShows)
                .HasForeignKey(x => x.CinemaHallID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Bookings)
                .WithOne(x => x.MovieShow)
                .HasForeignKey(x => x.MovieShowID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ShowSeats)
                .WithOne(x => x.MovieShow)
                .HasForeignKey(x => x.MovieShowID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
