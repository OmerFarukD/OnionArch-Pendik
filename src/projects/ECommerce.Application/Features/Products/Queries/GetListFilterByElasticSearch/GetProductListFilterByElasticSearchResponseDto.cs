namespace ECommerce.Application.Features.Products.Queries.GetListFilterByElasticSearch;

public class GetProductListFilterByElasticSearchResponseDto
{
    public string Name { get; set; }    
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int  Stock { get; set; }
    
    public int SubCategoryId { get; set; }
}