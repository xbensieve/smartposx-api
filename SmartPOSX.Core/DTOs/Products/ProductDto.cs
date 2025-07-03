namespace SmartPOSX.Core.DTOs.Products
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public Guid? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public List<ProductVariationDto> ProductVariations { get; set; }
    }

    public class ProductVariationDto
    {
        public Guid Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public List<VariationAttributeDto> VariationAttributes { get; set; }
        public List<VariationImageDto> VariationImages { get; set; }
    }

    public class VariationAttributeDto
    {
        public Guid Id { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }

    public class VariationImageDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
        public bool IsPrimary { get; set; }
    }
}
