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
    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(20).IsRequired();

            builder.Property(x => x.TotalCinemaHalls).HasDefaultValue(0);

            builder.HasOne(x => x.City)
                .WithMany(x => x.Cinemas)
                .HasForeignKey(x => x.CityID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.CinemaHalls)
                .WithOne(x => x.Cinema)
                .HasForeignKey(x => x.CinemaID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
