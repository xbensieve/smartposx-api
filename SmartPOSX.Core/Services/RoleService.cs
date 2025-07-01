using AutoMapper;
using SmartPOSX.Core.DTOs.Common;
using SmartPOSX.Core.DTOs.Roles;
using SmartPOSX.Core.Interfaces;
using SmartPOSX.Core.Interfaces.Services;
using SmartPOSX.Domain.Entities;

namespace SmartPOSX.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<object>> AddRoleAsync(CreateRoleDto request)
        {
            string combinedPermissions = string.Join(",", request.Permissions);

            var existingRole = _unitOfWork.Roles
                .Query()
                .Where(r => r.Name == request.Name)
                .FirstOrDefault();

            if (existingRole != null)
                return ApiResponse<object>.Fail("Role already exists.");

            var newRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Permissions = combinedPermissions,
            };

            await _unitOfWork.Roles.AddAsync(newRole);

            var saveResult = await _unitOfWork.SaveChangesAsync();

            if (saveResult == 0)
                return ApiResponse<object>.Fail("Failed to add role.");

            return ApiResponse<object>.Ok(new { roleId = newRole.Id }, "Role added successfully.");
        }

        public async Task<ApiResponse<List<RoleMapperModel>>> GetAllRolesAsync()
        {
            var roles = await _unitOfWork.Roles.GetAllAsync();
            var mappingRoles = _mapper.Map<List<RoleMapperModel>>(roles);
            return ApiResponse<List<RoleMapperModel>>.Ok(mappingRoles, "Roles retrieved successfully.");
        }

        public async Task<ApiResponse<RoleMapperModel>> GetRoleByIdAsync(Guid roleId)
        {
            var selectedRole = await _unitOfWork.Roles.GetByIdAsync(roleId);
            var mappingRole = _mapper.Map<RoleMapperModel>(selectedRole);

            if (mappingRole == null)
            {
                return ApiResponse<RoleMapperModel>.Fail("Role not found.");
            }
            return ApiResponse<RoleMapperModel>.Ok(mappingRole, "Role retrieved successfully.");
        }
    }
}
