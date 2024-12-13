using System.Runtime.CompilerServices;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
{
    public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
    {
        builder.ToTable("UserOperationClaims").HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(x => x.OperationClaimId).HasColumnName("OperationClaimId").IsRequired();

        builder.HasQueryFilter(u => !u.DeletedDate.HasValue);

        builder.HasOne(u => u.User)
            .WithMany(u => u.UserOperationClaims)
            .HasForeignKey(U => U.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.OperationClaim)
            .WithMany(u => u.UserOperationClaims);

        builder.HasQueryFilter(u => !u.DeletedDate.HasValue);
        
        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");


        builder.Navigation(x => x.OperationClaim).AutoInclude();
        builder.Navigation(x => x.User).AutoInclude();


        builder.HasData(getData());
    }

    private HashSet<UserOperationClaim> getData()
    {
        HashSet<UserOperationClaim> operationClaims = new();
        UserOperationClaim userOperationClaim = new(1,userId:1,operationClaimId:3);
        UserOperationClaim userOperationClaim2 = new(2,userId:1,operationClaimId:1);

        operationClaims.Add(userOperationClaim);
        operationClaims.Add(userOperationClaim2);

        return operationClaims;
    }
}