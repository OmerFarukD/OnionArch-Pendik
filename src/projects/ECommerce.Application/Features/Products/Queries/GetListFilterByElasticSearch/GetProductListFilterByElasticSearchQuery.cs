using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using MediatR;
using Nest;

namespace ECommerce.Application.Features.Products.Queries.GetListFilterByElasticSearch;

public class GetProductListFilterByElasticSearchQuery : MediatR.IRequest<List<GetProductListFilterByElasticSearchResponseDto>>
{
    public string Text { get; set; }
    
    
    
    public class GetProductListFilterByElasticSearchQueryHandler:
        IRequestHandler<GetProductListFilterByElasticSearchQuery,List<GetProductListFilterByElasticSearchResponseDto>>
    {

        private readonly IElasticClient _elasticClient;

        public GetProductListFilterByElasticSearchQueryHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<List<GetProductListFilterByElasticSearchResponseDto>> Handle(GetProductListFilterByElasticSearchQuery request, CancellationToken cancellationToken)
        {
            var text = request.Text;
            var searchResponse = await _elasticClient.SearchAsync<GetProductListFilterByElasticSearchResponseDto>(s=>
                s.Index("products").Query(
                    b=>b.Bool(
                        b=>b.Should(
                            
                            bs=> 
                                bs.Match(m=>
                                    m.Field(f=>f.Name).Query(text).Fuzziness(Fuzziness.Auto)),
                            bs=>   bs.Match(m=>
                                m.Field(f=>f.Description).Query(text).Fuzziness(Fuzziness.Auto))
                        ))
                    )
                    );

            if (!searchResponse.IsValid)
            {
                throw new BusinessException(searchResponse.ServerError.Error.Reason);
            }


            return searchResponse.Documents.ToList();
        }
    }
}