using GymManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GymManagement.DAL.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IReadOnlyList<TEntity>> GetAllAsync( CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> GetAllIncludingAsync(
       Expression<Func<TEntity, object>>[]? includes = null,
       CancellationToken ct = default);

    Task<TEntity?> GetByIdAsync(
        int id,
        Expression<Func<TEntity,bool>>? predicate = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default);
    Task<TEntity?> GetByIdIncludeDeletedAsync(int id, CancellationToken ct = default);
    Task<TEntity?> FindAsync(Expression<Func<TEntity,bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool>ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken ct = default);
    Task SoftDeleteAsync(TEntity entity,CancellationToken ct = default);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
