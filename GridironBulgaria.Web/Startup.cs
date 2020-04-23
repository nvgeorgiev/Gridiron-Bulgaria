namespace GridironBulgaria.Web
{
    using GridironBulgaria.Web.Data;
    using GridironBulgaria.Web.Services.Games;
    using GridironBulgaria.Web.Services.PhotoAlbums;
    using GridironBulgaria.Web.Services.Teams;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            });

            services.AddRouting(options =>
            {
                options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
            });

            services.AddTransient<ITeamsService, TeamsService>();
            services.AddTransient<IPhotoAlbumsService, PhotoAlbumsService>();
            services.AddTransient<IGamesService, GamesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller:slugify}/{action:slugify}/{id:slugify?}",
                     defaults: new { controller = "Home", action = "Index" });

                endpoints.MapRazorPages();
            });

            CreateRoles(services).Wait();
        }

        // This method is used for seeding Admin role and an admin User in the database.
        private async Task CreateRoles(IServiceProvider serviceProvider)
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

        // This method is used to add slugified urls
        public class SlugifyParameterTransformer : IOutboundParameterTransformer
        {
            public string TransformOutbound(object value)
            {
                return value == null ? null : Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
            }
        }
    }
}
