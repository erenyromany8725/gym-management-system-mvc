using GymManagement.DAL.Models;
using GymManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Repositories.Classes;

public class MemberRepository(GymDbContext context) : Repository<Member>(context), IMemberRepository
{
    private readonly GymDbContext _dbContext = context;

    public Task<bool> IsEmailTakenAsync(string normalizedEmail, int? execludedId = null, CancellationToken ct = default)
        => _dbContext.Set<Member>().AnyAsync(m => m.Email == normalizedEmail && (!execludedId.HasValue || m.Id != execludedId.Value), ct);

    public Task<bool> IsPhoneTakenAsync(string normalizedPhone, int? execludedId = null, CancellationToken ct = default)
       =>   _dbContext.Set<Member>().AnyAsync(m=>m.Phone == normalizedPhone && (!execludedId.HasValue || m.Id != execludedId.Value),ct);

    public async Task<Member?> GetWithMembershipAsync(int id ,
        DateTime now,
        CancellationToken ct = default)
    
       => await  _dbContext.Set<Member>()
            .AsNoTracking()
            .Include(m => m.MembersShips
                .Where(ms => ms.StartDate <= now && ms.EndDate >= now ))
                 .ThenInclude(ms => ms.Plan)
            .FirstOrDefaultAsync(m => m.Id == id,ct);
    

  
}
