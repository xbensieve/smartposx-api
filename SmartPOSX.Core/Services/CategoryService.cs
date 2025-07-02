using AutoMapper;
using SmartPOSX.Core.DTOs.Categories;
using SmartPOSX.Core.DTOs.Common;
using SmartPOSX.Core.Interfaces;
using SmartPOSX.Core.Interfaces.Services;
using SmartPOSX.Domain.Entities;

namespace SmartPOSX.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<object>> CreateCategoryAsync(CreateCategoryDto request)
        {
            if (request == null)
                return ApiResponse<object>.Fail("All fields required.");

            var existingCategory = await _unitOfWork.Categories
                .FindAsync(c => c.Name == request.Name);

            if (existingCategory != null)
                return ApiResponse<object>.Fail("Category with this name already exists.");

            var newCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name.Trim(),
                Description = request.Description?.Trim(),
                CreatedAt = DateTime.UtcNow,
            };

            try
            {
                await _unitOfWork.Categories.AddAsync(newCategory);
                int result = await _unitOfWork.SaveChangesAsync();
                if (result == 0)
                    return ApiResponse<object>.Fail("Failed to create category.");
                return ApiResponse<object>.Ok(new { categoryId = newCategory.Id }, "Category created successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.Fail($"{ex.Message}");
            }
        }

        public Task<ApiResponse<object>> DeleteCategoryAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();

            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

            if (categoryDtos == null || categoryDtos.Count == 0)
                return ApiResponse<List<CategoryDto>>.Fail("No categories found.");

            return ApiResponse<List<CategoryDto>>.Ok(categoryDtos, "Categories retrieved successfully.");
        }

        public async Task<ApiResponse<CategoryDto>> GetCategoryByIdAsync(Guid id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);

            var categoryDto = _mapper.Map<CategoryDto>(category);

            if (categoryDto == null)
                return ApiResponse<CategoryDto>.Fail("Category not found.");

            return ApiResponse<CategoryDto>.Ok(categoryDto, "Category retrieved successfully.");
        }

        public Task<ApiResponse<object>> UpdateCategoryAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
