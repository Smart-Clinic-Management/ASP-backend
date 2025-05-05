using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartClinic.Infrastructure.Configuration;

public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.HasIndex(x => x.IsActive);
    }
}