using RealEstate.BLL.Interfaces;

namespace RealEstate.BLL.InterFaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}
