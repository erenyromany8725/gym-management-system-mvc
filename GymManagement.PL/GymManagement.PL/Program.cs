using GymManagement.BLL;
using GymManagement.DAL;
using GymManagement.DAL.Repositories.Classes;
using GymManagement.DAL.Repositories.Interfaces;
using GymManagement.DAL.Seed;
using GymManagementSystem.Interceptors;
using Microsoft.Extensions.Options;

namespace GymManagementSystem;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();


        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddGymDataAccess(connectionString);
        builder.Services.AddGymBusinessLogic();

        var app = builder.Build();

        
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        if (app.Environment.IsDevelopment()) 
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
            await dbcontext.Database.MigrateAsync();
            await DatabaseSeeder.SeedAllAsync(dbcontext);
        }
        

        app.Run();
    }
}
