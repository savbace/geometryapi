using Geometry.Queries.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Geometry.Queries;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<GeometryDatabase>(options => options.UseSqlServer(configuration.GetConnectionString("GeometryDatabase")))
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RectangleEntity>());
    }
}
