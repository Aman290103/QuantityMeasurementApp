using QuantityMeasurementApp.Entity;
using System.Threading.Tasks;

namespace QuantityMeasurementApp.Service
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto);
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto);
    }
}
