using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, ICategoryService categoryService, IImageService imageService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
            _imageService = imageService;
            _mapper = mapper;
        }
        public async Task<ApiResponse<object>> CreateProductAsync(CreateProductDto request)
        {
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
                var newVariationId = Guid.NewGuid();

                var newVariation = new ProductVariation
                {
                    Id = newVariationId,
                    ProductId = newProduct.Id,
                    Sku = GenerateSku(category.Data.Name, variation.Name),
                    Description = variation.Description,
                    Name = variation.Name,
                    Price = variation.Price,
                    Stock = variation.Stock,
                    VariationAttributes = CreateVariationAttributes(variation.VariationAttributes, newVariationId),
                    VariationImages = CreateVariationImages(variation.VariationImages, newVariationId)
                };

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

        private List<VariationAttribute> CreateVariationAttributes(List<CreateVariationAttributeDto>? attributes, Guid variationId)
        {
            if (attributes == null || attributes.Count == 0) return new();

            return attributes.Select(attr => new VariationAttribute
            {
                Id = Guid.NewGuid(),
                ProductVariationId = variationId,
                AttributeName = attr.AttributeName,
                AttributeValue = attr.AttributeValue
            }).ToList();
        }

        private List<VariationImage> CreateVariationImages(List<CreateVariationImageDto>? images, Guid variationId)
        {
            if (images == null || images.Count == 0) return new();

            return images.Select(img => new VariationImage
            {
                Id = Guid.NewGuid(),
                ProductVariationId = variationId,
                ImageUrl = img.ImageUrl,
                PublicId = img.PublicId
            }).ToList();
        }

        public async Task<ApiResponse<List<ProductDto>>> GetProductListAsync()
        {
            var products = await _unitOfWork.Products.Query()
                .Include(p => p.Category)
                .Include(p => p.ProductVariations)
                .ThenInclude(v => v.VariationAttributes)
                .Include(p => p.ProductVariations)
                .ThenInclude(v => v.VariationImages)
                .ToListAsync();

            var productDtos = _mapper.Map<List<ProductDto>>(products);

            if (productDtos == null || productDtos.Count == 0)
            {
                return ApiResponse<List<ProductDto>>.Fail("No products found.");
            }

            return ApiResponse<List<ProductDto>>.Ok(productDtos, "Products retrieved successfully.");
        }

        public async Task<ApiResponse<ProductDto>> GetProductByIdAsync(Guid productId)
        {
            var product = _unitOfWork.Products.Query()
                .Include(p => p.Category)
                .Include(p => p.ProductVariations)
                .ThenInclude(v => v.VariationAttributes)
                .Include(p => p.ProductVariations)
                .ThenInclude(v => v.VariationImages)
                .FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return ApiResponse<ProductDto>.Fail("Product not found.");
            }

            var productDto = _mapper.Map<ProductDto>(product);

            return ApiResponse<ProductDto>.Ok(productDto, "Product retrieved successfully.");
        }
    }
}
