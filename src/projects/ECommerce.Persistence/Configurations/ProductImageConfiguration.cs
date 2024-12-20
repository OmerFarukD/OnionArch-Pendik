using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImages").HasKey(i => i.Id);
        builder.Property(i => i.Id).HasColumnName("Id").IsRequired();
        builder.Property(i => i.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(i => i.Url).HasColumnName("Url").IsRequired();
        builder.HasOne(i => i.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasQueryFilter(i => !i.DeletedDate.HasValue);


        builder.Navigation(x => x.Product).AutoInclude();
    }
}