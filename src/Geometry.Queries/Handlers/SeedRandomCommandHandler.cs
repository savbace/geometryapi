using Geometry.Queries.Database;
using MediatR;

namespace Geometry.Queries.Handlers;

public class SeedRandomCommand : IRequest
{
    public SeedRandomCommand(int count, bool overwrite)
    {
        this.Count = count;
        this.Overwrite = overwrite;
    }

    public int Count { get; set; }

    public bool Overwrite { get; }
}

internal class SeedRandomCommandHandler : IRequestHandler<SeedRandomCommand>
{
    private readonly GeometryDatabase database;

    public SeedRandomCommandHandler(GeometryDatabase database)
    {
        this.database = database;
    }

    public async Task Handle(SeedRandomCommand request, CancellationToken cancellationToken)
    {
        var rectangles = GenerateRandom(request.Count);

        if (request.Overwrite)
        {
            await this.DeleteAllRectangles();
        }

        this.database.Rectangles.AddRange(rectangles);
        await this.database.SaveChangesAsync(cancellationToken);
    }

    private Task DeleteAllRectangles()
    {
        this.database.Rectangles.RemoveRange(this.database.Rectangles);
        return this.database.SaveChangesAsync();
    }

    private static IEnumerable<RectangleEntity> GenerateRandom(int count)
        => Enumerable
            .Range(1, count)
            .Select(GenerateRect);

    private static RectangleEntity GenerateRect(int _) => new()
    {
        X = RandomCoordinate(),
        Y = RandomCoordinate(),
        Width = RandomSide(),
        Height = RandomSide(),
    };

    private static double RandomSide() => Random.Shared.Next(1, 50);

    private static int RandomCoordinate() => Random.Shared.Next(-200, 200);
}
