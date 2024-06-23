using Microsoft.EntityFrameworkCore;

namespace Core.GenericRepository;

public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext
{
    protected readonly TContext context;

    public GenericRepository(TContext context)
    {
        this.context = context;
    }

    #region get
    public TEntity? Get(Guid id)
    {
        return context.Set<TEntity>().Find(id);
    }

    public async Task<TEntity?> GetAsync(Guid id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }
    #endregion

    #region getAll
    public IList<TEntity?> GetAll()
    {
        return context.Set<TEntity>().AsNoTracking().ToList()!;
    }

    public async Task<IList<TEntity?>> GetAllAsync()
    {
        return (await context.Set<TEntity>().AsNoTracking().ToListAsync())!;
    }
    #endregion

    #region insert
    public void Insert(TEntity entity)
    {
        context.Add(entity);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await context.AddAsync(entity);
    }
    #endregion

    #region update
    public void Update(TEntity entity)
    {
        context.Update(entity);
    }

    #endregion

    #region delete
    public void Delete(TEntity entity)
    {
        context.Remove(entity);
    }

    #endregion
}