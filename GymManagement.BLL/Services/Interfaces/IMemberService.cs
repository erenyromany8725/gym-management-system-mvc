
using GymManagement.BLL.Common;
using GymManagement.BLL.ViewModel.HealthRecord;
using GymManagement.BLL.ViewModel.Member;
using GymManagement.DAL.Repositories.Classes;
using GymManagement.DAL.Repositories.Interfaces;

namespace GymManagement.BLL.Servicess.Interfaces;

public interface IMemberService
{
   Task<Result<IEnumerable<MemberIndexVM>>> GetAllAsync(CancellationToken ct = default);
    Task<Result> CreateAsync(CreateMemberVM model, CancellationToken ct = default);
    Task<Result<MemberDetailsVM>> GetDetailsAsync(int id, CancellationToken ct = default);
    Task<Result<HealthRecordDetailsVM>> GetHealthRecordAsync(int id, CancellationToken ct = default);
    Task<Result<EditMemberVM>> GetForUpdateAsync(int id, CancellationToken ct = default);
    Task<Result>UpdateAsync(int id,EditMemberVM model, CancellationToken ct = default);
    Task<Result>DeleteAsync(int id, CancellationToken ct = default);
}
