using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Contracts.DataSeed;
using ECommerce.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ECommerce.Persistence.IdentityData.IdentityDataSeed
{
    public class IdentityDataSeed : IDataSeed
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataSeed> _logger;

        public IdentityDataSeed(UserManager<ApplicationUser> userManager, 
                                RoleManager<IdentityRole> roleManager,
                                ILogger<IdentityDataSeed> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task SeedDataAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var user01 = new ApplicationUser
                    {
                        DisplayName = "Omar Ashraf",
                        UserName = "OmarAshraf",
                        Email = "OmarAshraf@gmail.com",
                        PhoneNumber = "01141131608"
                    };
                    var user02 = new ApplicationUser
                    {
                        DisplayName = "Youssef Sayed",
                        UserName = "YoussefSayed",
                        Email = "YoussefSayed@gmail.com",
                        PhoneNumber = "01141131609"
                    };
                    await _userManager.CreateAsync(user01, "P@ssw0rd");
                    await _userManager.CreateAsync(user02, "P@ssw0rd");
                    await _userManager.AddToRoleAsync(user01, "Admin");
                    await _userManager.AddToRoleAsync(user02, "SuperAdmin");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error While Seeding Data, Message :{ex.Message}");
            }
        }
    }
}
