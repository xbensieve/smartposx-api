using SmartPOSX.Core.DTOs.Categories;
using SmartPOSX.Core.DTOs.Common;

namespace SmartPOSX.Core.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<ApiResponse<object>> CreateCategoryAsync(CreateCategoryDto request);
        Task<ApiResponse<List<CategoryDto>>> GetAllCategoriesAsync();
        Task<ApiResponse<CategoryDto>> GetCategoryByIdAsync(Guid id);
        Task<ApiResponse<object>> UpdateCategoryAsync(Guid id);
        Task<ApiResponse<object>> DeleteCategoryAsync(Guid id);
    }
}
