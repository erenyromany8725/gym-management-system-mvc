using GymManagement.DAL.Models;
using GymManagement.DAL.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Repositories.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IMemberRepository Members { get; }
    IPlanRepository Plans { get; }
    ITraineerRepository Traineers { get; }
    IBookingRepository Bookings { get; }
    ISessionRepository Sessions { get; }
    IRepository<Category> Categories { get; }
    IRepository<HealthRecord> HealthRecords { get; }
   
    Task<int> CommitAsync(CancellationToken ct);
    Task BeginTransactionAsync(CancellationToken ct);
    Task CommitTransactionAsync(CancellationToken ct);
    Task RollBackTransactionAsync(CancellationToken ct);

}
