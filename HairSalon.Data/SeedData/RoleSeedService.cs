using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HairSalon.Data.SeedData
{
    public class RoleSeedService : IRoleSeedService
    {
        public RoleSeedService(RoleManager<IdentityRole<int>> roleManager, IOptions<UserRoleSettings> userRoleSettings) 
        {
            RoleManager = roleManager;
            UserRoleSettings = userRoleSettings.Value;
        }
        
        public RoleManager<IdentityRole<int>> RoleManager { get; }
        public UserRoleSettings UserRoleSettings { get; }


        public async Task SeedRoleAsync()
        {

            if (!await RoleManager.RoleExistsAsync(UserRoleSettings.AdminRoleName))
                await RoleManager.CreateAsync(new IdentityRole<int> { Name = UserRoleSettings.AdminRoleName });

            if (!await RoleManager.RoleExistsAsync(UserRoleSettings.AszisztensRoleName))
                await RoleManager.CreateAsync(new IdentityRole<int> { Name = UserRoleSettings.AszisztensRoleName });

            if (!await RoleManager.RoleExistsAsync(UserRoleSettings.PrivilegedUserRoleName))
                await RoleManager.CreateAsync(new IdentityRole<int> { Name = UserRoleSettings.PrivilegedUserRoleName });
        }
    }
}
