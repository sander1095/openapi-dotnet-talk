using ApiModels;

using Microsoft.AspNetCore.Mvc;

namespace ControllersDotnet9.Controllers;

[ApiController]
[Route("api/talks")]
public class TalksController : ControllerBase
{
    /// <summary>
    /// Gets all talks
    /// </summary>
    /// <returns>A list of all available talks</returns>
    [HttpGet(Name = "Talks_GetTalks")]
    [ProducesResponseType<IReadOnlyCollection<TalkModel>>(StatusCodes.Status200OK, Description = "Successfully retrieved all talks")]
    [ProducesDefaultResponseType]
    public ActionResult GetTalks()
    {
        return Ok(SampleTalks.Talks);
    }

    /// <summary>
    /// Gets a specific talk by ID
    /// </summary>
    /// <param name="id">The ID of the talk to retrieve</param>
    /// <returns>The requested talk</returns>
    [HttpGet("{id:int:min(1)}", Name = "Talks_GetTalk")]
    [ProducesResponseType<TalkModel>(StatusCodes.Status200OK, Description = "Successfully retrieved the talk")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Description = "The talk was not found")]
    public ActionResult<CreateTalkModel> GetTalk(int id)
    {
        var talk = SampleTalks.Talks.FirstOrDefault(x => x.Id == id);
        if (talk == null)
        {
            // Comment out the 404 response type to get the following warning
            // thanks to <IncludeOpenAPIAnalyzers> in the csproj.
            // Warning API1000 Action method returns undeclared status code '404' 
            return NotFound();
        }

        return Ok(talk);
    }

    /// <summary>
    /// Creates a talk
    /// </summary>
    /// <param name="requestBody">The requestbody for the talk</param>
    /// <returns>The created talk</returns>
    [HttpPost(Name = "Talks_CreateTalk")]
    [ProducesResponseType<TalkModel>(StatusCodes.Status200OK, Description = "Successfully created the talk")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Description = "The request was invalid")]
    [ProducesResponseType(StatusCodes.Status409Conflict, Description = "A talk with this title already exists")]
    [ProducesDefaultResponseType]
    public ActionResult<TalkModel> CreateTalk(CreateTalkModel requestBody)
    {
        // 400 bad request validation is done automatically thanks to [ApiController]
        if (SampleTalks.Talks.Any(x => x.Title == requestBody.Title))
        {
            return Conflict();
        }

        var newTalk = new TalkModel()
        {
            Id = SampleTalks.Talks.Count + 1,
            Title = requestBody.Title,
            LengthInMinutes = requestBody.LengthInMinutes,
            RoomName = requestBody.RoomName
        };

        SampleTalks.Talks.Add(newTalk);

        return Ok(newTalk);
    }
}