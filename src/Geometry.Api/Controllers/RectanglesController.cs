using Geometry.Core;
using Geometry.Queries.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Geometry.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RectanglesController : ControllerBase
    {
        private readonly ILogger<RectanglesController> logger;
        private readonly IMediator mediator;

        public RectanglesController(ILogger<RectanglesController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpPost("containing")]
        public async Task<IEnumerable<RectangleMatch>> GetInside(IReadOnlyList<Point> points)
            => await this.mediator.Send(new MatchRectanglesQuery(points));

        [HttpPost("seed")]
        // todo: auth
        public Task Seed(int count = 200, bool overwrite = false) 
            => this.mediator.Send(new SeedRandomCommand(count, overwrite));
    }
}