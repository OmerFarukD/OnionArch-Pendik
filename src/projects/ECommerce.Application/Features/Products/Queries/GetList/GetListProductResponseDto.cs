namespace ECommerce.Application.Features.Products.Queries.GetList;

public class GetListProductResponseDto
{
    public string Name { get; set; }    
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int  Stock { get; set; }
    public string CategoryName { get; set; }
}