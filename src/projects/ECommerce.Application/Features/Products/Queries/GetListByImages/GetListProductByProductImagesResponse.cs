namespace ECommerce.Application.Features.Products.Queries.GetListByImages;


// Ürünler listelenirken Url ler ise bana bir listede gelsin.

public class GetListProductByProductImagesResponse
{
    public string Name { get; set; }    
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int  Stock { get; set; }
    public string CategoryName { get; set; }
    public List<string> Urls { get; set; }
}