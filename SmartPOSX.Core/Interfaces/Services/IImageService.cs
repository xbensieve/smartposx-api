using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace SmartPOSX.Core.Interfaces.Services
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);
        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
}
