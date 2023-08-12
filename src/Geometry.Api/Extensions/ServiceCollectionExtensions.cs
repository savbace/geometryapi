using Geometry.Queries;

namespace Geometry.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRectangleServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddQueries(configuration);
    }
}
