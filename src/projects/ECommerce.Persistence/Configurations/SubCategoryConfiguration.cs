using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        builder.ToTable("SubCategories").HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("Id").IsRequired();
        builder.Property(s => s.Name).HasColumnName("Name").IsRequired();
        builder.Property(s => s.CategoryId).HasColumnName("CategoryId").IsRequired();
        builder.HasOne(s => s.Category)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(s => s.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(s => s.Products)
            .WithOne(p => p.SubCategory)
            .HasForeignKey(p => p.SubCategoryId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasQueryFilter(s => !s.DeletedDate.HasValue);
    }
}