using AutoMapper;
using ECommerce.Application.Features.Products.Commands.Create;
using ECommerce.Application.Features.Products.Queries.GetList;
using ECommerce.Application.Features.Products.Queries.GetListByImages;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Products.Profiles;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<ProductAddCommand, Product>();
        CreateMap<Product, ProductAddResponseDto>();


        CreateMap<Product, GetListProductByProductImagesResponse>()
            .ForMember(p=>p.CategoryName,
                opt=>opt.MapFrom(x=>x.SubCategory.Name)
                )
            .ForMember(p => p.Urls,
                opt =>
                    opt.MapFrom(pi => pi.ProductImages.Select(x => x.Url).ToList()));

        CreateMap<Product, GetListProductResponseDto>()
            .ForMember(p=>p.CategoryName,
                opt=> opt.MapFrom(x=>x.SubCategory.Name));
    } 
}