using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories").HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
        builder.Property(c => c.Name).HasColumnName("Name").IsRequired();
        builder.HasMany(c => c.SubCategories)
            .WithOne(s => s.Category)
            .HasForeignKey(s => s.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasQueryFilter(c => !c.DeletedDate.HasValue);
    }
}