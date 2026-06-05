
using GymManagement.BLL.Common;
using GymManagement.BLL.ViewModel.Member;

namespace GymManagement.BLL.Sericess.Interfaces;

public interface IMemberService
{
    public Task<IEnumerable<MemberIndexVM>> GetAllAsync(CancellationToken ct = default);
    public Task<Result> CreateAsync(CreateMemberVM model, CancellationToken ct = default);

}
