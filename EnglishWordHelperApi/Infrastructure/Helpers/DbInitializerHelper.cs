using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EnglishWordHelperApi.Infrastructure.Helpers
{
    public static class DbInitializerHelper
    {
        public static void SeedAdmins(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            var email = configuration["Admins:Denis:Email"];
            var userName = configuration["Admins:Denis:UserName"];
            var password = configuration["Admins:Denis:Password"];

            if (userManager.FindByEmailAsync(email).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = userName,
                    Email = email
                };

                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
