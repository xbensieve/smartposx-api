using SmartPOSX.Core.DTOs.Common;
using SmartPOSX.Core.DTOs.Roles;

namespace SmartPOSX.Core.Interfaces.Services
{
    public interface IRoleService
    {
        Task<ApiResponse<object>> AddRoleAsync(CreateRoleDto request);
        Task<ApiResponse<List<RoleMapperModel>>> GetAllRolesAsync();
        Task<ApiResponse<RoleMapperModel>> GetRoleByIdAsync(Guid roleId);
    }
}
