using System.Net.ServerSentEvents;
using System.Runtime.CompilerServices;

using ApiModels;

namespace WhatsNewInDotnet11;

public static class ServerSentEventsEndpoints
{
    public static WebApplication MapServerSentEventsEndpoints(this WebApplication app)
    {
        app.MapGet("/api/talks/live", (CancellationToken cancellationToken) =>
            TypedResults.ServerSentEvents(GetLiveTalks(cancellationToken)))
        .WithName("Talks_Live")
        .WithTags("Talks");

        return app;
    }

    private static async IAsyncEnumerable<SseItem<TalkModel>> GetLiveTalks(
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (var talk in SampleTalks.Talks)
        {
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            yield return new SseItem<TalkModel>(talk, eventType: "talk");
        }
    }
}
