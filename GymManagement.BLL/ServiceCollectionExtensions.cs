using GymManagement.BLL.Common;
using GymManagement.BLL.Mapping;
using GymManagement.BLL.Servicess.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.BLL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGymBusinessLogic(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IMemberService, MemberService>();
        services.AddAutoMapper(conf => { }, typeof(MemberProfile).Assembly);

        return services;
    }
}
