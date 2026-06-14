using GymManagement.BLL.Common;
using GymManagement.BLL.Mapping;
using GymManagement.BLL.Services.Classes;
using GymManagement.BLL.Services.Interfaces;
using GymManagement.BLL.Servicess.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.BLL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGymBusinessLogic(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<IPlanService, PlanService>();
        services.AddAutoMapper(conf => { }, typeof(MemberProfile).Assembly);

        return services;
    }
}
