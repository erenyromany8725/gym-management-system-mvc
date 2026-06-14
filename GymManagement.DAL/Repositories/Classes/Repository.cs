using GymManagement.DAL.Models;
using GymManagement.DAL.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace GymManagement.DAL.Repositories.Classes;

public class Repository<TEntity>(GymDbContext context) : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly GymDbContext _dbContext = context;
    private readonly DbSet<TEntity> _dbSet = context .Set<TEntity>();
    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
    => await _dbSet.AsNoTracking().ToListAsync(ct);
    public async Task<IReadOnlyList<TEntity>> GetAllIncludingAsync(
      Expression<Func<TEntity, object>>[]? includes = null,
      CancellationToken ct = default)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync(ct);
    }
    public async Task<TEntity?> GetByIdAsync(
    int id,
    Expression<Func<TEntity, bool>>? predicate = null,
    Expression<Func<TEntity, object>>[]? includes = null,
    CancellationToken ct = default)
    {
        var query = ApplyIncludes(_dbSet.AsQueryable(), includes);
        if (predicate != null)
            query = query.Where(predicate);
            
            return await query.FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task<TEntity?> GetByIdIncludeDeletedAsync(int id, CancellationToken ct = default)
     => await _dbSet.IgnoreQueryFilters().FirstOrDefaultAsync(t => t.Id==id , ct);

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
    => await _dbSet.AnyAsync(predicate, ct);
    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    => await _dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken);
    public async Task AddAsync(TEntity entity, CancellationToken ct = default)
    => await _dbSet.AddAsync(entity, ct);

    public async Task SoftDeleteAsync(TEntity entity, CancellationToken ct = default) 
    {
        entity.IsDeleted = true;
        _dbSet.Update(entity);
    }

    public async Task UpdateAsync(TEntity entity,CancellationToken ct = default)
    => _dbSet.Update(entity);

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    => _dbContext.SaveChangesAsync(ct);

   private static IQueryable<TEntity> ApplyIncludes(
       IQueryable<TEntity> query,
       IEnumerable<Expression<Func<TEntity,object>>> includes)
    {
        if(includes is null)
            return query;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query;

    }


}