using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products").HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
        builder.Property(p => p.Price).HasColumnName("Price").IsRequired();
        builder.Property(p => p.Description).HasColumnName("Description").IsRequired();
        builder.Property(p => p.Stock).HasColumnName("Stock").IsRequired();
        builder.Property(p => p.SubCategoryId).HasColumnName("SubCategoryId").IsRequired();
        builder.HasOne(p => p.SubCategory)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SubCategoryId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(p => p.ProductTags)
            .WithOne(t => t.Product)
            .HasForeignKey(t => t.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(p => p.ProductImages)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(p => p.OrderItems)
            .WithOne(o => o.Product)
            .HasForeignKey(o => o.ProductId)
            .OnDelete(DeleteBehavior.NoAction);



        builder.Navigation(x => x.SubCategory).AutoInclude();
        builder.HasQueryFilter(p => !p.DeletedDate.HasValue);
    }
}