using ApiModels;

namespace WhatsNewInDotnet11;

public static class ContentTypeEndpoints
{
    public static WebApplication MapContentTypeEndpoints(this WebApplication app)
    {
        app.MapGet("/api/talks/{id:int}/export", (int id, HttpRequest request) =>
        {
            var talk = SampleTalks.Talks.FirstOrDefault(x => x.Id == id);
            if (talk is null)
            {
                return Results.NotFound();
            }

            return request.Headers.Accept.ToString().Contains("text/csv")
                ? Results.Text($"{talk.Id},{talk.Title},{talk.LengthInMinutes},{talk.RoomName}", "text/csv")
                : Results.Ok(talk);
        })
        .WithName("Talks_Export")
        .WithTags("Talks")
        .Produces<TalkModel>(StatusCodes.Status200OK, "application/json")
        .Produces<string>(StatusCodes.Status200OK, "text/csv")
        .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}
