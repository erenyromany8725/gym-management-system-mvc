using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Repositories.Interfaces;

public interface IMembershipRepository
{
    Task<bool> HasActiveMembershipAsync(int planId,DateTime now ,CancellationToken ct);

}
