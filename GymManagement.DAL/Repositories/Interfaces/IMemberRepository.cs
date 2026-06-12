using GymManagement.DAL.Models;

namespace GymManagement.DAL.Repositories.Interfaces;

public interface IMemberRepository : IRepository<Member>
{
    Task<bool> IsEmailTakenAsync(string normalizedEmail, int? execludedId = null, CancellationToken ct = default);
    Task<bool> IsPhoneTakenAsync(string phone, int? execludedId = null, CancellationToken ct = default);
    Task<Member?> GetWithMembershipAsync(int id, DateTime now, CancellationToken ct = default);
    
}
