using Core.Persistence.Repositories;

namespace ECommerce.Domain.Entities;

public sealed class SubCategory : Entity<int>
{
    public string Name { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public ICollection<Product> Products { get; set; }
}