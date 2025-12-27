using System.ComponentModel;

using ApiModels;

using Microsoft.AspNetCore.Http.HttpResults;

namespace MinimalApiDotnet10;

/// <remarks>
/// This example doesn't have feature parity with the .NET 10 controllers project.
/// It misses things like OpenAPI's "default" and returning a Problem Details with 404/409.
/// This would need an OpenAPI transformer and more complex code, respectively, which doesn't fit in this small demo.
/// </remarks>
public static class TalkEndpoints
{
    public static WebApplication MapTalkEndpoints(this WebApplication app)
    {
        var api = app.MapGroup("api/talks");

        api.MapGet("/", GetTalks).WithName("Talks_GetTalks");
        api.MapGet("/{id:int:min(1)}", GetTalk).WithName("Talks_GetTalk");
        api.MapPost("/", CreateTalk).WithName("Talks_CreateTalk").WithSummary("Creates a talk");

        return app;
    }

    public static Ok<List<TalkModel>> GetTalks()
    {
        return TypedResults.Ok(SampleTalks.Talks);
    }

    public static Results<Ok<TalkModel>, NotFound> GetTalk(int id)
    {
        var talk = SampleTalks.Talks.FirstOrDefault(x => x.Id == id);

        return talk == null ?
            TypedResults.NotFound() :
            TypedResults.Ok(talk);
    }

    public static Results<Ok<TalkModel>, ValidationProblem, Conflict> CreateTalk([Description("The requestbody for the talk")] CreateTalkModel requestBody)
    {
        // Note: This endpoint contains no request validation for brevity reasons, as Minimal API doesn't support this out of the box yet, unlike controllers!
        // TODO: Remove this comment and check it
        if (SampleTalks.Talks.Any(x => x.Title == requestBody.Title))
        {
            return TypedResults.Conflict();
        }

        var newTalk = new TalkModel()
        {
            Id = SampleTalks.Talks.Count + 1,
            Title = requestBody.Title,
            LengthInMinutes = requestBody.LengthInMinutes,
            RoomName = requestBody.RoomName
        };

        SampleTalks.Talks.Add(newTalk);

        return TypedResults.Ok(newTalk);
    }
}
