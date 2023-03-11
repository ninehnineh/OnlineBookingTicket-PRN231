using BusinessObject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class OnlineBookingTicketDbContext : IdentityDbContext<AppUsers>
    {
        public OnlineBookingTicketDbContext(DbContextOptions<OnlineBookingTicketDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AppUserRoles").HasKey(x => new { x.RoleId, x.UserId });
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AppUserLogins").HasKey(x => new { x.ProviderKey, x.LoginProvider });
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AppUserTokens").HasKey(x => new { x.Name, x.LoginProvider, x.UserId });
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AppRoleClaims");

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
