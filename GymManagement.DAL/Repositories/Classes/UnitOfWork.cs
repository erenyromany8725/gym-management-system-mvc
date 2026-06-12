using GymManagement.DAL.Models;
using GymManagement.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Repositories.Classes;

public class UnitOfWork(GymDbContext context) : IUnitOfWork
{
    private readonly GymDbContext _dbContext = context;
    private IDbContextTransaction? _dbTransaction;
    
    private IMemberRepository? _members;
    private IPlanRepository? _plans;
    private ITraineerRepository? _traineers;
    private IBookingRepository? _bookings;
    private ISessionRepository? _sessions;
    private IRepository<Category>? _categories;
    private IRepository<HealthRecord>? _healthRecords;

   
    public IMemberRepository Members => _members ?? new MemberRepository(_dbContext);
    public IPlanRepository Plans => _plans ?? new PlanRepository(_dbContext);
    public ITraineerRepository Traineers => _traineers ?? new TraineerRepository(_dbContext);
    public IBookingRepository Bookings => _bookings ?? new BookingRepository(_dbContext);
    public ISessionRepository Sessions => _sessions ?? new SessionRepository(_dbContext);
    public IRepository<Category> Categories => _categories ?? new Repository<Category>(_dbContext);
    public IRepository<HealthRecord> HealthRecords => _healthRecords ?? new Repository<HealthRecord>(_dbContext);
    
    
    public async Task<int> CommitAsync(CancellationToken ct)
       => await _dbContext.SaveChangesAsync(ct);
    public async Task BeginTransactionAsync(CancellationToken ct)
    {
        _dbTransaction = await _dbContext.Database.BeginTransactionAsync(ct);
    }
    public async Task CommitTransactionAsync(CancellationToken ct)
    {
        if (_dbTransaction is null)
            throw new InvalidOperationException("No active transaction to commit.");

        await _dbTransaction.CommitAsync(ct);
        await _dbTransaction.DisposeAsync();
        _dbTransaction = null;
    }
    public async Task RollBackTransactionAsync(CancellationToken ct)
    {
        if (_dbTransaction is null)
            throw new InvalidOperationException("No active transaction to roll back.");

        await _dbTransaction.RollbackAsync(ct);
        await _dbTransaction.DisposeAsync();
        _dbTransaction = null;
    }
    public async ValueTask DisposeAsync()
    {
        if (_dbTransaction is not null)
        {
            await _dbTransaction.DisposeAsync();
            _dbTransaction = null;
        }

        await _dbContext.DisposeAsync();
    }
}