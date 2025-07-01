using SmartPOSX.Core.DTOs.Common;
using SmartPOSX.Core.DTOs.Employees;

namespace SmartPOSX.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<object>> CreateEmployeeAccount(CreateEmployeeDto request);
        Task<ApiResponse<object>> Login(LoginRequest request);
    }
}
