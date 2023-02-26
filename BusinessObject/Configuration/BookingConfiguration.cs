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
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NumberOfSeats).IsRequired().HasDefaultValue(0);

            builder.Property(x => x.Timestamp).IsRequired().HasColumnType("date");

            builder.HasOne(x => x.MovieShow)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.MovieShowID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ShowSeats)
                .WithOne(x => x.Booking)
                .HasForeignKey(x => x.BookingID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
