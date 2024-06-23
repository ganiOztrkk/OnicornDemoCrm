namespace Core.GenericRepository;

public interface IGenericRepository <TEntity> where TEntity: class, IEntity, new()
{
    public TEntity? Get(Guid id);
    public Task <TEntity?> GetAsync(Guid id);
    
    public IList<TEntity?> GetAll();
    public Task<IList<TEntity?>> GetAllAsync();
    
    public void Insert(TEntity entity);
    public Task InsertAsync(TEntity entity);
    
    public void Update(TEntity entity);
    
    public void Delete(TEntity entity);
}