using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Entity;
using System.Threading.Tasks;

namespace QuantityMeasurementApp.Repository
{
    public class EfCoreUserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public EfCoreUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserEntity> CreateAsync(UserEntity user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
