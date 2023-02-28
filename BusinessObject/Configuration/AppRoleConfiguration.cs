using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Configuration
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.ToTable("AppRoles");

            builder.HasData(

                new IdentityRole
                {
                    Id = "1",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },

                new IdentityRole
                {
                    Id = "2",
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                }

                );
        }
    }
}
