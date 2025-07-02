using Microsoft.AspNetCore.Http;

namespace SmartPOSX.Core.DTOs.Products
{
    public class UploadImageDto
    {
        public IFormFile File { get; set; }
    }
}
