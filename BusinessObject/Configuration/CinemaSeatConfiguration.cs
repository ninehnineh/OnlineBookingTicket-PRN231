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
    public class CinemaSeatConfiguration : IEntityTypeConfiguration<CinemaSeat>
    {
        public void Configure(EntityTypeBuilder<CinemaSeat> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.SeatNumber)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(x => x.Type).IsRequired();

            builder.HasOne(x => x.CinemaHall)
                .WithMany(x => x.CinemaSeats)
                .HasForeignKey(x => x.CinemaHallID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ShowSeats)
                .WithOne(x => x.CinemaSeat)
                .HasForeignKey(x => x.CinemaSeatID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
