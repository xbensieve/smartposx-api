﻿using SmartPOSX.Core.DTOs.Common;
using SmartPOSX.Core.DTOs.Products;

namespace SmartPOSX.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<ApiResponse<object>> CreateProductAsync(CreateProductDto request);
        Task<ApiResponse<List<ProductDto>>> GetProductListAsync();
        Task<ApiResponse<ProductDto>> GetProductByIdAsync(Guid productId);
    }
}
