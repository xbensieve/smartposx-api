using Microsoft.AspNetCore.Mvc;
using SmartPOSX.Core.Interfaces.Services;

namespace SmartPOSX.API.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
        [HttpPost]
        public async Task<IActionResult> UploadImages([FromForm] List<IFormFile> files)
        {
            if (files == null || !files.Any())
            {
                return BadRequest(new { message = "No files provided." });
            }

            var results = new List<object>();
            foreach (var file in files)
            {
                var result = await _imageService.UploadImageAsync(file);
                if (result != null && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    results.Add(new { Url = result.SecureUrl.ToString(), PublicId = result.PublicId });
                }
            }
            return Ok(results);
        }
        [HttpDelete("{publicId}")]
        public async Task<IActionResult> DeleteImage(string publicId)
        {
            try
            {
                var result = await _imageService.DeleteImageAsync(publicId);
                return Ok(new { message = "Image deleted", result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
