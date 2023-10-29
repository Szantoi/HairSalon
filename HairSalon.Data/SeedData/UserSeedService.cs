using HairSalon.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HairSalon.Data.SeedData
{
    public class UserSeedService : IUserSeedService
    {
        public UserSeedService(UserManager<HairSalonUser> userManager, IOptions<UserRoleSettings> userRoleSettings)
        {
            UserManager = userManager;
            UserRoleSettings = userRoleSettings.Value;
        }

        public UserManager<HairSalonUser> UserManager { get; }
        public UserRoleSettings UserRoleSettings { get; }

        public async Task SeedUserAsync()
        {
            if (!(await UserManager.GetUsersInRoleAsync(UserRoleSettings.AdminRoleName)).Any())
            {
                var HairSalonUser = new HairSalonUser
                {
                    UserName = UserRoleSettings.AdminUserName,
                    Email = UserRoleSettings.AdminEmail,
                    FullName = UserRoleSettings.AdminFullName,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var createResult = await UserManager.CreateAsync(HairSalonUser, UserRoleSettings.AdminPasword);

                if (UserManager.Options.SignIn.RequireConfirmedAccount)
                {
                    //Regisztraciot meg kell erositeni.
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(HairSalonUser);
                    var result = await UserManager.ConfirmEmailAsync(HairSalonUser, code);
                }

                var addToRoleReult = await UserManager.AddToRoleAsync(HairSalonUser, UserRoleSettings.AdminRoleName);

                if (!createResult.Succeeded || !addToRoleReult.Succeeded)
                {
                    throw new ApplicationException("Nem sikerult letrehozni az adminisztrator HairSalonUsert: " +
                        string.Join(", ", createResult.Errors.Concat(addToRoleReult.Errors).Select(e => e.Description)));
                }
            }

            if (!(await UserManager.GetUsersInRoleAsync(UserRoleSettings.AszisztensRoleName)).Any())
            {
                var HairSalonUser = new HairSalonUser
                {
                    UserName = UserRoleSettings.AszisztensUserName,
                    Email = UserRoleSettings.AszisztensEmail,
                    FullName = UserRoleSettings.AszisztensFullName,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var createResult = await UserManager.CreateAsync(HairSalonUser, UserRoleSettings.AszisztensPasword);

                if (UserManager.Options.SignIn.RequireConfirmedAccount)
                {
                    //Regisztraciot meg kell erositeni.
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(HairSalonUser);
                    var result = await UserManager.ConfirmEmailAsync(HairSalonUser, code);
                }

                var addToRoleReult = await UserManager.AddToRoleAsync(HairSalonUser, UserRoleSettings.AdminRoleName);
                var addToAszisztensRoleReult = await UserManager.AddToRoleAsync(HairSalonUser, UserRoleSettings.AszisztensRoleName);

                if (!createResult.Succeeded || !addToRoleReult.Succeeded || !addToAszisztensRoleReult.Succeeded)
                {
                    throw new ApplicationException("Nem sikerult letrehozni az Aszisztens HairSalonUsert: " +
                        string.Join(", ", 
                        createResult.Errors
                        .Concat(addToRoleReult.Errors)
                        .Concat(addToAszisztensRoleReult.Errors)
                        .Select(e => e.Description)));
                }
            }

            if (!(await UserManager.GetUsersInRoleAsync(UserRoleSettings.PrivilegedUserRoleName)).Any())
            {
                var HairSalonUser = new HairSalonUser
                {
                    UserName = UserRoleSettings.PrivilegedUserUserName,
                    Email = UserRoleSettings.PrivilegedUserEmail,
                    FullName = UserRoleSettings.PrivilegedUserFullName,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var createResult = await UserManager.CreateAsync(HairSalonUser, UserRoleSettings.PrivilegedUserPasword);

                if (UserManager.Options.SignIn.RequireConfirmedAccount)
                {
                    //Regisztraciot meg kell erositeni.
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(HairSalonUser);
                    var result = await UserManager.ConfirmEmailAsync(HairSalonUser, code);
                }

                var addToRoleReult = await UserManager.AddToRoleAsync(HairSalonUser, UserRoleSettings.AdminRoleName);
                var addToDoktorRoleReult = await UserManager.AddToRoleAsync(HairSalonUser, UserRoleSettings.PrivilegedUserRoleName);

                if (!createResult.Succeeded || !addToRoleReult.Succeeded || !addToDoktorRoleReult.Succeeded)
                {
                    throw new ApplicationException("Nem sikerult letrehozni az Doktor HairSalonUsert: " +
                        string.Join(", ", 
                        createResult.Errors
                        .Concat(addToRoleReult.Errors)
                        .Concat(addToDoktorRoleReult.Errors)
                        .Select(e => e.Description)));
                }
            }
        }
    }
}
