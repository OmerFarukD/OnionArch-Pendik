using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders").HasKey(o => o.Id);
        builder.Property(o => o.Id).HasColumnName("Id").IsRequired();
        builder.Property(o => o.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(o => o.Total).HasColumnName("Total").IsRequired();
        builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasQueryFilter(o => !o.DeletedDate.HasValue);
    }
}