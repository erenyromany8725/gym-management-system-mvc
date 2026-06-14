using GymManagement.DAL.Models;
using GymManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Repositories.Classes;

public class MembershipRepository(GymDbContext context) : Repository<Membership>(context), IMembershipRepository 
{
    private readonly GymDbContext _dbContext = context;
    public async Task<bool> HasActiveMembershipAsync(int planId, DateTime now,CancellationToken ct)
    => await _dbContext.Memberships.AnyAsync(ms => ms.PLanId == planId && ms.EndDate > now ,ct);
      
    
}
