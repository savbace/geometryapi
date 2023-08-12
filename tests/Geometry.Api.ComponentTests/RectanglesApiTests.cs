namespace Geometry.Api.ComponentTests
{
    public class RectanglesApiTests: IClassFixture<GeometryWebApplicationFactory>
    {
        private readonly GeometryWebApplicationFactory factory;

        public RectanglesApiTests(GeometryWebApplicationFactory factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task Containing_Smoke_Test()
        {
            var client = factory.CreateClient();

            var points = new[]
            {
                new { X = 4, Y = 3 },
                new { X = 6, Y = 6 }
            };

            HttpResponseMessage response = await client.PostAsJsonAsync("/api/rectangles/containing", points);

            string content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            Assert.NotEmpty(content);
        }
    }
}