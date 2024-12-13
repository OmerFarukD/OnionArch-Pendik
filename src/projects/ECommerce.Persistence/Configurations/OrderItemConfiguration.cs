using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems").HasKey(i => i.Id);
        builder.Property(i => i.Id).HasColumnName("Id").IsRequired();
        builder.Property(i => i.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(i => i.Count).HasColumnName("Count").IsRequired();
        builder.Property(i => i.OrderId).HasColumnName("OrderId").IsRequired();
        builder.HasOne(i => i.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(i => i.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasQueryFilter(i => !i.DeletedDate.HasValue);
    }
}