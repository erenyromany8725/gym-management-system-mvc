using GymManagement.DAL.Repositories.Interfaces;

namespace GymManagement.DAL.Repositories.Classes;

public class PlanRepository(GymDbContext context) : Repository<Plan>(context), IPlanRepository
{



}
