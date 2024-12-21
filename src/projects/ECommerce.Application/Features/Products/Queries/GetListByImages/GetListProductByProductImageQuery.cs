using AutoMapper;
using Core.Application.Pipelines.Caching;
using ECommerce.Application.Services.Repositories;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetListByImages;

public class GetListProductByProductImageQuery : IRequest<List<GetListProductByProductImagesResponse>>,ICachableRequest
{
    public string CacheKey => "GetAllProductsListByImages";
    public bool ByPassCache => true;
    public string? CacheGroupKey => "Products";
    public TimeSpan? SlidingExpiration { get; }
    
    
    public class GetListProductByProductImageQueryHandler: IRequestHandler<GetListProductByProductImageQuery,List<GetListProductByProductImagesResponse>>
    {

        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetListProductByProductImageQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<List<GetListProductByProductImagesResponse>> Handle(GetListProductByProductImageQuery request, CancellationToken cancellationToken)
        {
            var products =
                await _productRepository.GetListAsync(enableTracking: false, cancellationToken: cancellationToken);

            var response = _mapper.Map<List<GetListProductByProductImagesResponse>>(products);

            return response;
        }
    }

}