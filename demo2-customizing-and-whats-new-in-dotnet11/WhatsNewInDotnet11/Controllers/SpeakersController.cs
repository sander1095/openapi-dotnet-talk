using Microsoft.AspNetCore.Mvc;

namespace WhatsNewInDotnet11.Controllers;

[ApiController]
[Route("api/speakers")]
public class SpeakersController : ControllerBase
{
    private static readonly List<SpeakerModel> Speakers =
    [
        new("Sander", "Netherlands"),
        new("Anna", "Sweden"),
        new("Erik", "Sweden"),
        new("Mette", "Denmark")
    ];

    // Attribute arguments must be constants, and HttpMethods.Query is static readonly,
    // so [AcceptVerbs(HttpMethods.Query)] fails with CS0182. The string works.
    [AcceptVerbs("QUERY", Route = "search", Name = "Speakers_Search")]
    public ActionResult<List<SpeakerModel>> Search(SpeakerSearch search)
    {
        return Speakers
            .Where(x => search.Name is null || x.Name.Contains(search.Name, StringComparison.OrdinalIgnoreCase))
            .Where(x => search.Country is null || x.Country == search.Country)
            .ToList();
    }
}

public record SpeakerModel(string Name, string Country);

public record SpeakerSearch(string? Name, string? Country);
