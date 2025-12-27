using System.ComponentModel.DataAnnotations;

namespace ApiModels;

public class CreateTalkModel
{
    [StringLength(100, MinimumLength = 1)]
    public required string Title { get; set; }

    [Range(1, 640)]
    public required int LengthInMinutes { get; set; }

    /// <summary>
    /// The name of the room where the talk will take place.
    /// </summary>
    public required string RoomName { get; set; }
}
