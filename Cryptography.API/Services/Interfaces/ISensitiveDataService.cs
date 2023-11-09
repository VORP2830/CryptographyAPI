using Cryptography.API.DTOs;

namespace Cryptography.API.Services.Interfaces
{
    public interface ISensitiveDataService
    {
       Task<IEnumerable<SensitiveDataDTO>> GetAll();
       Task<SensitiveDataDTO> GetById(long id);
       Task<SensitiveDataDTO> Create(SensitiveDataDTO model);
       Task<SensitiveDataDTO> Update(SensitiveDataDTO model);
       Task<SensitiveDataDTO> Delete(long id);
    }
}