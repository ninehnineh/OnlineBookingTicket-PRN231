using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class OnlineBookingTicketDbContextFactory : IDesignTimeDbContextFactory<OnlineBookingTicketDbContext>
    {
        public OnlineBookingTicketDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..\\OnlineBookingTicketAPI"))
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<OnlineBookingTicketDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            builder.UseSqlServer(connectionString);

            return new OnlineBookingTicketDbContext(builder.Options);
        }
    }
}
