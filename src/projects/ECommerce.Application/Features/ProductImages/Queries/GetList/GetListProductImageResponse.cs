namespace ECommerce.Application.Features.ProductImages.Queries.GetList;

public record struct GetListProductImageResponse
{
    public int Id { get; init; }
    public string Url { get; init; } 
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }
}