using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
{
    public void Configure(EntityTypeBuilder<ProductTag> builder)
    {
        builder.ToTable("ProductTags").HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("Id").IsRequired();
        builder.Property(t => t.TagId).HasColumnName("TagId").IsRequired();
        builder.Property(t => t.ProductId).HasColumnName("ProductId").IsRequired();
        builder.HasOne(t => t.Product)
            .WithMany(p => p.ProductTags)
            .HasForeignKey(t => t.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(t => t.Tag)
            .WithMany(tag => tag.ProductTags)
            .HasForeignKey(t => t.TagId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasQueryFilter(i => !i.DeletedDate.HasValue);
    }
}