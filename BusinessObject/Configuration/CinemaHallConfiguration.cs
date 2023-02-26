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
    public class CinemaHallConfiguration : IEntityTypeConfiguration<CinemaHall>
    {
        public void Configure(EntityTypeBuilder<CinemaHall> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Totalseats).IsRequired().HasDefaultValue(0);

            builder.HasOne(x => x.Cinema)
                .WithMany(x => x.CinemaHalls)
                .HasForeignKey(x => x.CinemaID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.MovieShows)
                .WithOne(x => x.CinemaHall)
                .HasForeignKey(x => x.CinemaHallID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.CinemaSeats)
                .WithOne(x => x.CinemaHall)
                .HasForeignKey(x => x.CinemaHallID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
