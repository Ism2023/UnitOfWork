using System.Threading.Tasks;

namespace Pattern.Repositories.Contract
{
    public interface IUnitOfWork
    {
        void Dispose();
        void Save();
        Task SaveAsync();
        Task SaveUnauditedAsync();
        void Dispose(bool disposing);
        IRepository<T> Repository<T>() where T : class;
    }
}
