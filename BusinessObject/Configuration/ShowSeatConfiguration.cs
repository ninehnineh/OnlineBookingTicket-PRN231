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
    public class ShowSeatConfiguration : IEntityTypeConfiguration<ShowSeat>
    {
        public void Configure(EntityTypeBuilder<ShowSeat> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Price).IsRequired().HasPrecision(18, 2);

            builder.HasOne(x => x.Booking)
                .WithMany(x => x.ShowSeats)
                .HasForeignKey(x => x.BookingID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.MovieShow)
                .WithMany(x => x.ShowSeats)
                .HasForeignKey(x => x.MovieShowID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CinemaSeat)
                .WithMany(x => x.ShowSeats)
                .HasForeignKey(x => x.CinemaSeatID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
