using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geometry.Queries.Database.EntityConfigurations;

internal class RectangleEntityConfiguration : IEntityTypeConfiguration<RectangleEntity>
{
    public void Configure(EntityTypeBuilder<RectangleEntity> builder)
    {
        builder.HasKey(r => r.Id);
    }
}
