namespace GridironBulgaria.Test
{
    using GridironBulgaria.Web;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            // services.AddTransient<ITeamsService, TeamsService>();
            // services.AddTransient<IPhotoAlbumsService, PhotoAlbumsService>();
            // services.AddTransient<IGamesService, GamesService>();
        }

        public void ConfigureTest(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            base.Configure(app, env, services);
            CreateRolesTest(services).Wait();
        }

        private async Task CreateRolesTest(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityResult roleResult;

            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var powerUser = new IdentityUser
            {
                UserName = Configuration.GetSection("UserSettings")["UserEmail"],
                Email = Configuration.GetSection("UserSettings")["UserEmail"],
                EmailConfirmed = true,
            };

            var UserPassword = Configuration.GetSection("UserSettings")["UserPassword"];

            var user = await UserManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);

            if (user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(powerUser, UserPassword);
                if (createPowerUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(powerUser, "Admin");
                }
            }
            else
            {
                await UserManager.AddToRoleAsync(powerUser, "Admin");
            }
        }
    }
}
