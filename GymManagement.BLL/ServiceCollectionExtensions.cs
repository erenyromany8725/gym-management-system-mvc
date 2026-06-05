using GymManagement.BLL.Sericess.Classes;
using GymManagement.BLL.Sericess.Interfaces;
using GymManagement.DAL.Repositories.Classes;
using GymManagement.DAL.Repositories.Interfaces;
using GymManagementSystem.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.BLL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGymBusinessLogic(this IServiceCollection services)
    {
        services.AddScoped<IMemberService, MemberService>();
        return services;
    }
}
