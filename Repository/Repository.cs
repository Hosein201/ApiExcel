using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ApiExcel.Repository
{
    public interface IRepositoryImpert<TEntity> where TEntity : class
    {
        Task AddOrUpdateAsync(Stream stream, CancellationToken cancellationToken = default);
    }
    public interface IRepositoryRead
    {
        object Get();
    }
}

