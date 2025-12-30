using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiModels;

public class TalkModel
{
    public required int Id { get; set; }

    [MinLength(1)]
    [MaxLength(500)]
    public required string Title { get; set; }

    [Range(1, 640)]
    public required int LengthInMinutes { get; set; }

    /// <summary>
    /// The name of the room where the talk will take place.
    /// </summary>
    public required string RoomName { get; set; }
}
