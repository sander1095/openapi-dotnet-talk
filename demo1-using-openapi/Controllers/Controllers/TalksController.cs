using ApiModels;

using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers;

[ApiController]
[Route("api/talks")]
public class TalksController : ControllerBase
{
    [HttpGet(Name = "Talks_GetTalks")]
    [ProducesResponseType<IReadOnlyCollection<TalkModel>>(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public ActionResult GetTalks()
    {
        return Ok(SampleTalks.Talks);
    }

    [HttpGet("{id:int:min(1)}", Name = "Talks_GetTalk")]
    [ProducesResponseType<TalkModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TalkModel> GetTalk(int id)
    {
        var talk = SampleTalks.Talks.FirstOrDefault(x => x.Id == id);
        if (talk == null)
        {
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
    [ProducesResponseType<TalkModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Description = "Returned when a talk with the given title already exists")]
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