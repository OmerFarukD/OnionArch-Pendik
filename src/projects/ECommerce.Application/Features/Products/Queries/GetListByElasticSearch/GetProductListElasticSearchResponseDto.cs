namespace ECommerce.Application.Features.Products.Queries.GetListByElasticSearch;

public class GetProductListElasticSearchResponseDto
{
    public string Name { get; set; }    
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int  Stock { get; set; }
    
    public int SubCategoryId { get; set; }
}