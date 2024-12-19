using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Security.Constants;
using ECommerce.Application.Features.Products.Rules;
using ECommerce.Application.Services.Repositories;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Products.Commands.Create;

public class ProductAddCommand : IRequest<ProductAddResponseDto>,
    ISecuredRequest, ILoggableRequest, ICacheRemoverRequest
{
    public string Name { get; set; }    
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int  Stock { get; set; }
    
    public int SubCategoryId { get; set; }
    public string[] Roles => [GeneralOperationClaims.Admin];
    public string CacheKey => "";
    public bool ByPassCache => false;
    public string? CacheGroupKey => "Products";
    
    
    public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand,ProductAddResponseDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ProductBusinessRules _businessRules;

        public ProductAddCommandHandler(IProductRepository productRepository, IMapper mapper, ProductBusinessRules businessRules)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<ProductAddResponseDto> Handle(ProductAddCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);

            var created = await _productRepository.AddAsync(product);

            var response =  _mapper.Map<ProductAddResponseDto>(created);

            return response;
        }
    }

  
}