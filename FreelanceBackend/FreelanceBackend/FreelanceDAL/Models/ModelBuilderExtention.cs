using FreelanceDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceDAL.Models
{
    public static class ModelBuilderExtention
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "649fca11-51e3-4db8-91d7-0bba63163280",
                    Name = "ServiceProvider",
                    NormalizedName = "SERVICEPROVIDER"
                },
                new IdentityRole
                {
                    Id = "35403dae-8769-4773-b617-a5087d827284",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                }
                );

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = "422b7d11-6c54-436e-a04f-7619e7acf637",
                    Name = "Aakash",
                    UserName = "aakasht.aspirefox@gmail.com",
                    NormalizedUserName = "AAKASHT.ASPIREFOX@GMAIL.COM",
                    Email = "aakasht.aspirefox@gmail.com",
                    NormalizedEmail = "AAKASHT.ASPIREFOX@GMAIL.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "Aakash@123"),
                    LockoutEnabled = true
                },
                new AppUser
                {
                    Id = "5eedf264-7629-412a-937e-926ec371be3c",
                    Name = "Customer",
                    UserName = "CUSTOMER@gmail.com",
                    NormalizedUserName = "USER@GMAIL.COM",
                    Email = "Customer@gmail.com",
                    NormalizedEmail = "CUSTOMER@GMAIL.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "Customer@123"),
                    LockoutEnabled = true
                }
                );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "649fca11-51e3-4db8-91d7-0bba63163280",
                UserId = "422b7d11-6c54-436e-a04f-7619e7acf637"
            },
            new IdentityUserRole<string>
            {
                RoleId = "35403dae-8769-4773-b617-a5087d827284",
                UserId = "5eedf264-7629-412a-937e-926ec371be3c"
            }
            );

            modelBuilder.Entity<ServiceType>().HasData(
                new ServiceType
                {
                    ServiceTypeId = 1,
                    ServiceName = "Web Development"
                },
                new ServiceType
                {
                    ServiceTypeId = 2,
                    ServiceName = "Graphic Designer"
                },
                new ServiceType
                {
                    ServiceTypeId = 3,
                    ServiceName = "Carpenter"
                }
                );

            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    ServiceId = 1,
                    Title = "Web Services",
                    Description = "We provide Web Services.",
                    Price = 25000,
                    ImageUrl = "webdesign.png",
                    ServiceTypeId = 1,
                    UserId = "422b7d11-6c54-436e-a04f-7619e7acf637"
                }
            );
        }
    }
}
