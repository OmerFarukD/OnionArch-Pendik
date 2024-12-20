using AutoMapper;
using ECommerce.Application.Features.ProductImages.Commands.Create;
using ECommerce.Application.Features.ProductImages.Queries.GetList;
using ECommerce.Application.Features.Products.Commands.Create;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.ProductImages.Profiles;

public class ProductImageProfile : Profile
{
    public ProductImageProfile()
    {
        CreateMap<ProductImageAddCommand, ProductImage>();
        CreateMap<ProductImage,ProductImageAddedResponseDto>();
        CreateMap<ProductImage, GetListProductImageResponse>();
    }
}