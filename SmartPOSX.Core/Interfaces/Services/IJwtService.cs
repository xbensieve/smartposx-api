using SmartPOSX.Domain.Entities;

namespace SmartPOSX.Core.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(Employee employee);
    }
}
