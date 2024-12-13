using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags").HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("Id").IsRequired();
        builder.Property(t => t.Name).HasColumnName("Name").IsRequired();
        builder.HasMany(t => t.ProductTags)
            .WithOne(p => p.Tag)
            .HasForeignKey(p => p.TagId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasQueryFilter(s => !s.DeletedDate.HasValue);
    }
}