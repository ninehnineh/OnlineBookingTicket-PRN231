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
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();

            builder.Property(x => x.ReleaseDate).HasColumnType("date");

            builder.Property(x => x.DurationInMinutes).IsRequired();

            builder.Property(x => x.Image).IsRequired().HasColumnType("VARBINARY(MAX)");

            builder.Property(x => x.Country).HasMaxLength(50);

            builder.Property(x => x.Language).HasMaxLength(20);

            builder.Property(x => x.Genre)
                .HasMaxLength(20)
                .IsRequired();

            builder.ToTable("Movies")
                .HasKey(x => x.Id);

            builder.HasMany(x => x.MovieShows)
                .WithOne(a => a.Movie)
                .HasForeignKey(x => x.MovieID)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
