using SmartPOSX.Core.DTOs.Common;
using SmartPOSX.Core.DTOs.Products;
using SmartPOSX.Core.Interfaces;
using SmartPOSX.Core.Interfaces.Services;
using SmartPOSX.Domain.Entities;

namespace SmartPOSX.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;
        private readonly IImageService _imageService;
        public ProductService(IUnitOfWork unitOfWork, ICategoryService categoryService, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
            _imageService = imageService;
        }
        public async Task<ApiResponse<object>> CreateProductAsync(CreateProductDto request)
        {
            if (request == null)
                return ApiResponse<object>.Fail("Invalid product data.", 400);

            if (string.IsNullOrWhiteSpace(request.Name) || request.BasePrice <= 0 || request.CategoryId == Guid.Empty)
                return ApiResponse<object>.Fail("Name, BasePrice, and CategoryId are required.");

            if (request.ProductVariations == null || !request.ProductVariations.Any())
                return ApiResponse<object>.Fail("At least one product variation is required.");

            var category = await _categoryService.GetCategoryByIdAsync(request.CategoryId);
            if (category?.Data == null)
                return ApiResponse<object>.Fail("Category not found.");

            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Sku = GenerateSku(category.Data.Name, request.Name),
                Name = request.Name,
                Description = request.Description,
                BasePrice = request.BasePrice,
                CategoryId = request.CategoryId,
                ProductVariations = new List<ProductVariation>()
            };

            foreach (var variation in request.ProductVariations)
            {
                var newVariation = new ProductVariation
                {
                    Id = Guid.NewGuid(),
                    ProductId = newProduct.Id,
                    Sku = GenerateSku(category.Data.Name, variation.Name),
                    Description = variation.Description,
                    Name = variation.Name,
                    Price = variation.Price,
                    Stock = variation.Stock,
                    VariationAttributes = new List<VariationAttribute>(),
                    VariationImages = new List<VariationImage>()
                };

                if (variation.VariationAttributes != null)
                {
                    foreach (var attribute in variation.VariationAttributes)
                    {
                        var newAttribute = new VariationAttribute
                        {
                            Id = Guid.NewGuid(),
                            ProductVariationId = newVariation.Id,
                            AttributeName = attribute.AttributeName,
                            AttributeValue = attribute.AttributeValue
                        };
                        newVariation.VariationAttributes.Add(newAttribute);
                    }
                }

                if (variation.VariationImages != null)
                {
                    foreach (var image in variation.VariationImages)
                    {
                        var newImage = new VariationImage
                        {
                            Id = Guid.NewGuid(),
                            ProductVariationId = newVariation.Id,
                            ImageUrl = image.ImageUrl,
                            PublicId = image.PublicId
                        };
                        newVariation.VariationImages.Add(newImage);
                    }
                }

                newProduct.ProductVariations.Add(newVariation);
            }

            await _unitOfWork.Products.AddAsync(newProduct);

            try
            {
                int result = await _unitOfWork.SaveChangesAsync();

                if (result > 0)
                {
                    return ApiResponse<object>.Ok(new { ProductId = newProduct.Id }, "Product created successfully.");
                }
                else
                {
                    return ApiResponse<object>.Fail("Failed to create product.");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.Fail($"An error occurred while creating the product: {ex.Message}");
            }
        }
        private string GenerateSku(string category, string productName, int length = 12)
        {
            category = category.Replace(" ", "").ToUpper();
            productName = productName.Replace(" ", "").ToUpper();

            category = category.Length > 4 ? category.Substring(0, 4) : category;
            productName = productName.Length > 4 ? productName.Substring(0, 4) : productName;

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string randomChars = new string(Enumerable.Repeat(chars, length - 8).Select(s => s[random.Next(s.Length)]).ToArray());

            string sku = $"{category}{productName}{randomChars}";

            return sku.Length > length ? sku.Substring(0, length) : sku;
        }
    }
}
