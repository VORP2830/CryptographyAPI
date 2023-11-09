using Cryptography.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cryptography.API.Context
{
     public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<SensitiveData> SensitiveDatas { get; set; }
    }
}