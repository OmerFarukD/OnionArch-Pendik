using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ECommerce.Infrastructure.Cloudinary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Infrastructure.CloudinaryServices;

public sealed class CloudinaryService
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;
    private readonly Account _account;
    private readonly CloudinarySettings _cloudinarySettings;


    public CloudinaryService(IConfiguration configuration)
    {
        _cloudinarySettings = configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
        _account = new Account(_cloudinarySettings.CloudName,_cloudinarySettings.ApiKey,_cloudinarySettings.ApiSecret);
        _cloudinary = new CloudinaryDotNet.Cloudinary(_account);
    }
    

    public async Task<string> UploadImage(IFormFile formFile,string imageDirectory)
    {
        var imageUploadResult = new ImageUploadResult();

        if (formFile.Length>0)
        {
            using var stream = formFile.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(formFile.Name,stream),
                Folder = imageDirectory
            };

            imageUploadResult = await _cloudinary.UploadAsync(uploadParams);
            string url = _cloudinary.Api.UrlImgUp.BuildUrl(imageUploadResult.PublicId);
            return url;
        }

        return string.Empty;
    }
    
}