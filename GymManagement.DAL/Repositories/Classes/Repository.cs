using GymManagement.DAL.Models;
using GymManagement.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace GymManagement.DAL.Repositories.Classes;

public class Repository<TEntity>(GymDbContext context) : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly GymDbContext _dbContext = context;
    private readonly DbSet<TEntity> _dbSet = context .Set<TEntity>();
    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
    => await _dbSet.AsNoTracking().ToListAsync(ct);
    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default)
    => await _dbSet.FirstOrDefaultAsync(t => t.Id == id, ct);
    public async Task<TEntity?> GetByIdIncludeDeletedAsync(int id, CancellationToken ct = default)
     => await _dbSet.IgnoreQueryFilters().FirstOrDefaultAsync(t => t.Id==id , ct);

    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
    => await _dbSet.AnyAsync(predicate, ct);
    public async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    => await _dbSet.Where(predicate).ToListAsync(cancellationToken);
    public async Task AddAsync(TEntity entity, CancellationToken ct = default)
    => await _dbSet .AddAsync(entity, ct);

    public void SoftDelete(TEntity entity) 
    {
        entity.IsDeleted = true;
        _dbSet.Update(entity);   
    }

    public void Update(TEntity entity)
    => _dbSet .Update(entity);

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    => _dbContext.SaveChangesAsync(ct);
}