using GymManagement.DAL.Repositories.Classes;
using GymManagement.DAL.Repositories.Interfaces;
using GymManagementSystem.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGymDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<AuditInterceptor>();
        services.AddDbContext<GymDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString);
            options.AddInterceptors(sp.GetRequiredService<AuditInterceptor>());
        });

        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IMemberRepository, MemberRepository>();

        return services;
    }
}
