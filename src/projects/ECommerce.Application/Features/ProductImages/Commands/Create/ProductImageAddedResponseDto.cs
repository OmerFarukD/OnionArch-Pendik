namespace ECommerce.Application.Features.ProductImages.Commands.Create;

public class ProductImageAddedResponseDto
{
    public int Id { get; set; }
    public string Url { get; set; } 
    public Guid ProductId { get; set; }
}