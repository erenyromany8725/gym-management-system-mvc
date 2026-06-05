using GymManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GymManagement.DAL.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IReadOnlyList<TEntity>> GetAllAsync( CancellationToken ct = default);
    Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<TEntity?> GetByIdIncludeDeletedAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity,bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool>ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void SoftDelete(TEntity entity);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
