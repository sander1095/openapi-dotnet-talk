using ApiModels;

namespace WhatsNewInDotnet11;

public static class QueryEndpoints
{
    public static WebApplication MapQueryEndpoints(this WebApplication app)
    {
        // HTTP QUERY: a safe, idempotent request with a body. MapMethods maps any HTTP method.
        app.MapMethods("/api/talks/search", ["QUERY"], (TalkSearch search) =>
        {
            var talks = SampleTalks.Talks
                .Where(x => search.Title is null || x.Title.Contains(search.Title, StringComparison.OrdinalIgnoreCase))
                .Where(x => search.MaxLengthInMinutes is null || x.LengthInMinutes <= search.MaxLengthInMinutes)
                .ToList();

            return TypedResults.Ok(talks);
        })
        .WithName("Talks_Search")
        .WithTags("Talks");

        return app;
    }
}

public record TalkSearch(string? Title, int? MaxLengthInMinutes);
