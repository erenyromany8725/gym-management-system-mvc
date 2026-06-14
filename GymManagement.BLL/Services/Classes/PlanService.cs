using AutoMapper;
using GymManagement.BLL.Common;
using GymManagement.BLL.Services.Interfaces;
using GymManagement.BLL.ViewModel.Member;
using GymManagement.BLL.ViewModel.Plan;
using GymManagement.DAL.Repositories.Classes;
using GymManagement.DAL.Repositories.Interfaces;

namespace GymManagement.BLL.Services.Classes;

public class PlanService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IDateTimeProvider clock) : IPlanService
{
    public async Task<Result<IEnumerable<PlanDetailsVM>>> GetAllAsync(CancellationToken ct = default)
    {
        var plans = await unitOfWork.Plans.GetAllAsync();
        if (plans == null)
            return Result<IEnumerable<PlanDetailsVM>>.Failure("No Available Plans.");
        return Result<IEnumerable<PlanDetailsVM>>.Success(mapper.Map<IEnumerable<PlanDetailsVM>>(plans));
    }

    public async Task<Result<PlanDetailsVM>> GetDetailsAsync(int id, CancellationToken ct = default)
    {
       var plan = await unitOfWork.Plans.GetByIdAsync(id,ct:ct);
        if (plan == null)
            return Result<PlanDetailsVM>.Failure("Plan not Found.", nameof(id));
        
        return Result<PlanDetailsVM>.Success(mapper.Map<PlanDetailsVM>(plan));
    }

    public async Task<Result<EditPlanVM>> GetForUpdateAsync(int id, CancellationToken ct = default)
    {
        var plan = await unitOfWork.Plans.GetByIdAsync(id, ct:ct);
        if (plan == null)
            return Result<EditPlanVM>.Failure("Plan not Found.", nameof(plan));
        return Result<EditPlanVM>.Success(mapper.Map<EditPlanVM>(plan));
    }

    public async Task<Result> UpdateAsync(int id, EditPlanVM model, CancellationToken ct = default)
    {
        var plan = await unitOfWork.Plans.GetByIdAsync(id,ct: ct);
        
        if (plan == null)
            return Result<EditPlanVM>.Failure("Plan not Found.", nameof(plan));
        
        if (await unitOfWork.Memberships.HasActiveMembershipAsync(id, clock.UTCNow, ct))
            return Result.Failure("Cannot update a plan with active membership.", nameof(id));
        
        mapper.Map(model, plan);
        await unitOfWork.Plans.UpdateAsync(plan);
        await unitOfWork.Plans.SaveChangesAsync(ct);
        return Result.Success();


    }
    public async Task<Result> ChangeStatusAsync(int id, CancellationToken ct = default)
    {
        var plan = await unitOfWork.Plans.GetByIdAsync(id, ct: ct);
        
        if (plan == null)
            return Result<EditPlanVM>.Failure("Plan not Found.", nameof(plan));
        
        if (await unitOfWork.Memberships.HasActiveMembershipAsync(id, clock.UTCNow, ct))
            return Result.Failure("Cannot update a plan with active membership.", nameof(id));
        //deactivatePlan
        if (plan.IsActive)
        {
            plan.IsActive = false;
            await unitOfWork.Plans.UpdateAsync(plan);
            await unitOfWork.Plans.SaveChangesAsync();
            return Result.Success();
        }
        //activatePlan
        plan.IsActive = true;
        await unitOfWork.Plans.UpdateAsync(plan);
        await unitOfWork.Plans.SaveChangesAsync();
        return Result.Success();


    }








}
