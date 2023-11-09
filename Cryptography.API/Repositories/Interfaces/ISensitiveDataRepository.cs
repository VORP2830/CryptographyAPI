using Cryptography.API.Models;

namespace Cryptography.API.Repositories.Interfaces
{
    public interface ISensitiveDataRepository
    {
        Task<IEnumerable<SensitiveData>> GetAll();
        Task<SensitiveData> GetById(long id);
        void Add(SensitiveData sensitiveData);
        void Update(SensitiveData sensitiveData);
        Task<bool> SaveChangesAsync();
    }
}