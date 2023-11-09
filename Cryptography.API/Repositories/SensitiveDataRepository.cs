using Cryptography.API.Context;
using Cryptography.API.Models;
using Cryptography.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cryptography.API.Repositories
{
    public class SensitiveDataRepository : ISensitiveDataRepository
    {
        private readonly ApplicationDbContext _context;
        public SensitiveDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SensitiveData>> GetAll()
        {
            return await _context.SensitiveDatas.Where(sd => sd.Active == true).ToListAsync();
        }
        public async Task<SensitiveData> GetById(long id)
        {
            return await _context.SensitiveDatas.FirstOrDefaultAsync(sd => sd.Active == true && sd.Id == id);
        }
        public void Add(SensitiveData sensitiveData)
        {
            _context.Add(sensitiveData);
        }
        public void Update(SensitiveData sensitiveData)
        {
            _context.Update(sensitiveData);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}