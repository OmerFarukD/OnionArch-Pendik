using Core.ElasticSearch.Services.Abstracts;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetListByElasticSearch;

public class GetProductListElasticSearchQuery : IRequest<List<GetProductListElasticSearchResponseDto>>
{
    
    public class GetProductListElasticSearchQueryHandler: IRequestHandler<GetProductListElasticSearchQuery,List<GetProductListElasticSearchResponseDto>>
    {
        private readonly IElasticSearchClientService _elasticSearchClientService;

        public GetProductListElasticSearchQueryHandler(IElasticSearchClientService elasticSearchClientService)
        {
            _elasticSearchClientService = elasticSearchClientService;
        }

        public async Task<List<GetProductListElasticSearchResponseDto>> Handle(GetProductListElasticSearchQuery request, CancellationToken cancellationToken)
        {
            var response = await _elasticSearchClientService.SearchAsync<GetProductListElasticSearchResponseDto>("products");

            return response;
        }
    }
}