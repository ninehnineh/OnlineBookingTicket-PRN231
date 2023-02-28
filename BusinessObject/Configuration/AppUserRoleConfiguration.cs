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
    public class AppUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.ToTable("AppUserRoles");

            builder.HasData(

                new IdentityUserRole<string>
                {
                    UserId = "1",
                    RoleId = "1",
                },

                new IdentityUserRole<string>
                {
                    UserId = "2",
                    RoleId = "2",
                }

                );
        }
    }
}
