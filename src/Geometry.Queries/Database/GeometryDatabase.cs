using Microsoft.EntityFrameworkCore;

namespace Geometry.Queries.Database;

internal class GeometryDatabase : DbContext
{
    public GeometryDatabase(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<RectangleEntity> Rectangles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GeometryDatabase).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
