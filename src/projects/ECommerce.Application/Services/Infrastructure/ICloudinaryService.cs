using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Services.Infrastructure;

public interface ICloudinaryService
{
    Task<string> UploadImage(IFormFile formFile, string imageDirectory);
}