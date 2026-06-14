
using GymManagement.BLL.Common;
using GymManagement.BLL.ViewModel.Member;
using GymManagement.BLL.ViewModel.Plan;


namespace GymManagement.BLL.Services.Interfaces;

public interface IPlanService
{
    Task<Result<IEnumerable<PlanDetailsVM>>> GetAllAsync(CancellationToken ct = default);
    Task<Result<PlanDetailsVM>> GetDetailsAsync(int id, CancellationToken ct = default);
    Task<Result<EditPlanVM>> GetForUpdateAsync(int id, CancellationToken ct = default);
    Task<Result> UpdateAsync(int id, EditPlanVM model, CancellationToken ct = default);
    Task<Result> ChangeStatusAsync(int id, CancellationToken ct = default);
    
}
