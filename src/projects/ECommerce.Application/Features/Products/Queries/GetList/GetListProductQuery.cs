using AutoMapper;
using Core.Application.Pipelines.Caching;
using ECommerce.Application.Services.Repositories;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetList;

public class GetListProductQuery : IRequest<List<GetListProductResponseDto>>, ICachableRequest
{
    public string CacheKey => "GetAllProductsList";
    public bool ByPassCache => false;
    public string? CacheGroupKey => "Products";
    public TimeSpan? SlidingExpiration { get; }
    
    public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery,List<GetListProductResponseDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetListProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        public async Task<List<GetListProductResponseDto>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetListAsync(
                enableTracking:false,
                withDeleted:true,
                cancellationToken: cancellationToken
                );

            var response = _mapper.Map<List<GetListProductResponseDto>>(products);

            return response;
        }
    }



}