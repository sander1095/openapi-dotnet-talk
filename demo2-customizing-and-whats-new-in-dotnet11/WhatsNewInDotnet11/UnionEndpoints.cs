using ApiModels;

namespace WhatsNewInDotnet11;

public static class UnionEndpoints
{
    public static WebApplication MapUnionEndpoints(this WebApplication app)
    {
        app.MapGet("/api/talks/{id:int}/session", (int id) =>
        {
            var talk = SampleTalks.Talks.FirstOrDefault(x => x.Id == id);

            return talk is not null
                ? new Session(talk)
                : new Session(new Workshop("Hands-on OpenAPI in .NET 11", 240));
        })
        .WithName("Talks_GetSession")
        .WithTags("Talks");

        return app;
    }
}

public record Workshop(string Title, int LengthInMinutes);

// C# 15 union: a Session is either a TalkModel or a Workshop.
public union Session(TalkModel, Workshop);
