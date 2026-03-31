using QuantityMeasurementApp.Entity;
using System.Threading.Tasks;

namespace QuantityMeasurementApp.Repository
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetByEmailAsync(string email);
        Task<UserEntity> CreateAsync(UserEntity user);
    }
}
