using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartPOSX.Core.Interfaces;
using SmartPOSX.Core.Interfaces.Repositories;
using SmartPOSX.Core.Interfaces.Services;
using SmartPOSX.Core.Mapper;
using SmartPOSX.Core.Services;
using SmartPOSX.Infrastructure.Data;
using SmartPOSX.Infrastructure.Repositories;

namespace SmartPOSX.Infrastructure.Implementation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
