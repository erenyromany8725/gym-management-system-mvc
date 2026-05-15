using GymManagement.DAL.Repositories.Interfaces;
using GymManagementSystem.Contexts;
using GymManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.DAL.Repositories.Classes;

public class PlanRepository : IPlanRepository
{
    private readonly GymDbContext _context;
    public PlanRepository(GymDbContext context)
    {
        this._context = context;
    }
    public async Task<int> AddAsync(Plan plan, CancellationToken ct = default)
    {
        _context.Plans.Add(plan);
        return await _context.SaveChangesAsync(ct);

    }

    public async Task<int> DeleteAsync(Plan plan, CancellationToken ct = default)
    {
        _context.Plans.Remove(plan);
        return await _context.SaveChangesAsync(ct);

    }

    public async Task<IEnumerable<Plan>> GetAllAsync(bool tracking = false, CancellationToken ct = default)
    {
        IQueryable<Plan> query = tracking ? _context.Plans : _context.Plans.AsNoTracking();
        return await query.ToListAsync();
    }

    public async Task<Plan?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Plans.FindAsync(id, ct);
    }

    public async Task<int> UpdateAsync(Plan plan, CancellationToken ct = default)
    {
        _context.Plans.Update(plan);
        return await _context.SaveChangesAsync(ct);
    }
}
