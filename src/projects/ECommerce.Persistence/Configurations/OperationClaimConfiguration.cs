using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("Name").IsRequired();
        
        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);


        builder.HasData(getData());
    }


    private HashSet<OperationClaim> getData()
    {
        HashSet<OperationClaim> datas = new()
        {
            new OperationClaim { Id = 1, Name = "Admin" },
            new OperationClaim { Id = 2, Name = "User" },
            new OperationClaim{Id = 3, Name = "Most Valueable People(MVP)"}
        };
        return datas;
    }
}