using Geometry.Core;
using Geometry.Queries.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Geometry.Queries.Handlers;

public record RectangleMatch(Point Point, IReadOnlyList<Rectangle> Rectangles);

public record MatchRectanglesQuery(IReadOnlyList<Point> Points) : IRequest<IReadOnlyList<RectangleMatch>>;

internal class MatchRectanglesQueryHandler : IRequestHandler<MatchRectanglesQuery, IReadOnlyList<RectangleMatch>>
{
    private const int BatchSize = 1000; // todo: from config
    private readonly GeometryDatabase database;

    public MatchRectanglesQueryHandler(GeometryDatabase database)
    {
        this.database = database;
    }

    public async Task<IReadOnlyList<RectangleMatch>> Handle(MatchRectanglesQuery request, CancellationToken cancellationToken)
    {
        return await HandlePaged(request, cancellationToken);
    }

    private async Task<IReadOnlyList<RectangleMatch>> HandlePaged(MatchRectanglesQuery request, CancellationToken cancellationToken)
    {
        int skip = 0;
        bool hasMore = true;
        Dictionary<Point, List<Rectangle>> matches = new();

        do
        {
            var query = this.database.Rectangles.OrderBy(x => x.Id).Skip(skip).Take(BatchSize);
            RectangleEntity[] entities = await query.ToArrayAsync(cancellationToken);
            AppendMatches(matches, entities, request.Points);

            skip += BatchSize;
            hasMore = entities.Length == BatchSize;
        } while (hasMore);

        return matches.Select(x => new RectangleMatch(x.Key, x.Value)).ToArray();
    }

    private static void AppendMatches(Dictionary<Point, List<Rectangle>> matches, RectangleEntity[] entities, IReadOnlyList<Point> points)
    {
        foreach (var item in MatchContainingPoints(entities, points))
        {
            if (matches.ContainsKey(item.Point))
            {
                matches[item.Point].AddRange(item.Rectangles);
            }
            else
            {
                matches[item.Point] = new List<Rectangle>();
            }
        }
    }

    private static IEnumerable<RectangleMatch> MatchContainingPoints(IReadOnlyList<RectangleEntity> rectangles, IReadOnlyList<Point> points)
        => points
            .Select(point => new RectangleMatch(point, GetMatchedRectangles(rectangles, point).ToArray()));

    private static IEnumerable<Rectangle> GetMatchedRectangles(IReadOnlyList<RectangleEntity> rectangles, Point point)
    {
        return from RectangleEntity entity in rectangles
               let rect = new Rectangle(entity.X, entity.Y, entity.Width, entity.Height)
               where rect.Contains(point)
               select rect;
    }
}
