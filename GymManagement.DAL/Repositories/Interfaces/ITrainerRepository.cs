using GymManagement.DAL.Models;

namespace GymManagement.DAL.Repositories.Interfaces;

public interface ITrainerRepository : IRepository<Traineer>
{
    Task<bool> IsEmailTakenAsync(string normalizedEmail, int? execludedId = null, CancellationToken ct = default);
    Task<bool> IsPhoneTakenAsync(string phone, int? execludedId = null, CancellationToken ct = default);
}