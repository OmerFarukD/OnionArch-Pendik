using System.Xml.Schema;
using Core.Security.Hashing;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configurations;

public class AppUserConfguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("AppUsers");

        builder.Property(u => u.Id).HasColumnName("Id").IsRequired();
        builder.Property(u => u.FirstName).HasColumnName("FirstName").IsRequired();
        builder.Property(u => u.LastName).HasColumnName("LastName").IsRequired();
        builder.Property(u => u.Email).HasColumnName("Email").IsRequired();
        builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash").IsRequired();
        builder.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt").IsRequired();
        builder.Property(u => u.Status).HasColumnName("Status").HasDefaultValue(true);

        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");


        builder.HasMany(u => u.UserOperationClaims)
            .WithOne()
            .HasForeignKey(x => x.UserId);


        builder.HasData(getData());
    }

    private HashSet<AppUser> getData()
    {
        HashSet<AppUser> users = new HashSet<AppUser>();
        HashingHelper.CreatePasswordHash(
            password: "Password123",
            passwordHash: out byte[] passwordHash,
            passwordSalt: out byte[] passwordSalt
            );

        AppUser adminUser = new()
        {
            Id = 1,
            FirstName = "Murat Ateş",
            LastName = "En iyi Developer",
            Email = "gayet_yeterli_bir_tanıtım@gmail.com",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = true,
            CreatedDate = DateTime.UtcNow,
        };
        users.Add(adminUser);
        return users;
    }
}