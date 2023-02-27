using BusinessObject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUsers>
    {
        public void Configure(EntityTypeBuilder<AppUsers> builder)
        {
            builder.ToTable("AppUsers");

            var hasher = new PasswordHasher<AppUsers>();
            builder.HasData(
                

                new AppUsers
                {
                    Id = "1",
                    Email = "Customer@localhost.com",
                    NormalizedEmail = "CUSTOMER@LOCALHOST.COM",
                    FirstName = "System",
                    LastName = "Customer",
                    UserName = "Customer@localhost.com",
                    NormalizedUserName = "CUSTOMER@LOCALHOST.com",
                    PasswordHash = hasher.HashPassword(null, "P@assword1"),
                    EmailConfirmed = false
                },

                new AppUsers
                {
                    Id = "2",
                    Email = "Manager@localhost.com",
                    NormalizedEmail = "MANAGER@LOCALHOST.COM",
                    FirstName = "System",
                    LastName = "Manager",
                    UserName = "Manager@localhost.com",
                    NormalizedUserName = "MANAGER@LOCALHOST.com",
                    PasswordHash = hasher.HashPassword(null, "P@assword1"),
                    EmailConfirmed = false
                }


                );
        }
    }
}
