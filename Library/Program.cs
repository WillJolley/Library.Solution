using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Library
{
  class Program
  {
    static async Task Main(string[] args)
    {
      WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

      builder.Services.AddControllersWithViews();

      builder.Services.AddDbContext<LibraryContext>(
                        dbContextOptions => dbContextOptions
                          .UseMySql(
                            builder.Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"]
                          )
                        )
                      );

      builder.Services.AddIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<LibraryContext>()
            .AddDefaultTokenProviders();

      builder.Services.Configure<IdentityOptions>(options =>
      {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
      });

      WebApplication app = builder.Build();

  

      // app.UseDeveloperExceptionPage();
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication(); 
      app.UseAuthorization();

      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");

      using(var scope = app.Services.CreateScope())
      {
        var roleManager = 
          scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        var roles = new[] { "Librarian", "Patron" };

        foreach (var role in roles)
        {
          if (!await roleManager.RoleExistsAsync(role))
               await roleManager.CreateAsync(new IdentityRole(role));
        }
      }

      using(var scope = app.Services.CreateScope())
      {
        var userManager =
            scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string email = "god@poo.com";
        string password = "Bpado55@";

        if(await userManager.FindByEmailAsync(email) == null)
        {
          var user = new IdentityUser();
          user.UserName = email;
          user.Email = email;

          await userManager.CreateAsync(user, password);

          await userManager.AddToRoleAsync(user, "Librarian");
        }
      }

      app.Run();
    }
  }
}