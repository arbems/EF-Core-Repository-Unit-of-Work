using EFCoreRepositoryUnitofWork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreRepositoryUnitofWork.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Username)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .OwnsOne(b => b.Address);
    }
}
