using AutoMapper;
using GymManagement.BLL.Common;
using GymManagement.BLL.Services.Interfaces;
using GymManagement.DAL.Repositories.Interfaces;

namespace GymManagement.BLL.Services.Classes;

public class SessionService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IDateTimeProvider clock) : ISessionService
{
}
