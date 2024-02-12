using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBankingApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Infrastructure.Data.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(CreateUsers());
        }
        private ApplicationUser CreateUsers()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser()
            {
                Id = "au1",
                FirstName = "Admin",
                LastName = "User",
                UserName="AdminUser",
                NormalizedUserName = "adminuser",
                Email = "admin@mail.com",
                NormalizedEmail = "admin@mail.com"
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "Admin@1234");
            return user;
        }
    }
}
