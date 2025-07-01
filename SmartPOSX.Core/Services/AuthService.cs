using SmartPOSX.Core.DTOs.Common;
using SmartPOSX.Core.DTOs.Employees;
using SmartPOSX.Core.Interfaces;
using SmartPOSX.Core.Interfaces.Services;
using SmartPOSX.Domain.Entities;

namespace SmartPOSX.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        public AuthService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<ApiResponse<object>> CreateEmployeeAccount(CreateEmployeeDto request)
        {
            if (request == null) return ApiResponse<object>.Fail("All fields required.");

            var existingRole = await _unitOfWork.Roles.GetByIdAsync(request.RoleId);

            if (existingRole == null) return ApiResponse<object>.Fail("Invalid role ID.");

            var hashPassword = _passwordHasher.Hash(request.Password);

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Name = request.Name,
                PasswordHash = hashPassword,
                RoleId = request.RoleId,
                EmployeeCode = GenerateEmployeeCode(existingRole.Name, DateTime.UtcNow),
                CreatedAt = DateTime.UtcNow,
            };

            try
            {
                await _unitOfWork.Employees.AddAsync(employee);
                int result = await _unitOfWork.SaveChangesAsync();

                if (result == 0) return ApiResponse<object>.Fail("Failed to create employee account.");

                return ApiResponse<object>.Ok(
                    new { employeeId = employee.Id, employeeCode = employee.EmployeeCode }
                    , "Employee account created successfully.");

            }
            catch (Exception ex)
            {
                return ApiResponse<object>.Fail($"{ex.Message}");
            }
        }

        private string GenerateEmployeeCode(string str, DateTime time)
        {
            string part1 = Sanitize(str);
            string part2 = time.ToString("yyyyMMddHHmmss");
            string part3 = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();

            return $"{part1}-{part2}-{part3}";
        }
        private static string Sanitize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "ROLE";

            return input.Trim().ToUpper().Replace(" ", "").Substring(0, Math.Min(5, input.Length));
        }

        public async Task<ApiResponse<object>> Login(LoginRequest request)
        {
            if (request == null) return ApiResponse<object>.Fail("All fields required.");

            var employee = await _unitOfWork.Employees
                .FindAsync(e => e.Email == request.Email, e => e.Role);

            if (employee == null)
            {
                return ApiResponse<object>.Fail("Invalid email or password.");
            }

            if (!_passwordHasher.Verify(request.Password, employee.PasswordHash))
            {
                return ApiResponse<object>.Fail("Invalid email or password.");
            }

            var token = _jwtService.GenerateToken(employee);

            return ApiResponse<object>.Ok(
                new { accessToken = token, role = employee.Role.Name },
                "Login successful.");

        }
    }
}
