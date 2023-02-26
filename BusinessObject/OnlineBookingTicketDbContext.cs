using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class OnlineBookingTicketDbContext : DbContext
    {
        public OnlineBookingTicketDbContext(DbContextOptions<OnlineBookingTicketDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<CinemaSeat> CinemaSeats { get; set; }
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<MovieShow> MovieShows { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ShowSeat> ShowSeats { get; set; }

    }
}
