namespace ApiModels;
public class SampleTalks
{
    public static readonly List<TalkModel> Talks =
    [
        new() { Id = 1, Title = "Computers in Amber", LengthInMinutes = 40, RoomName = "Turing" },
        new() { Id = 2, Title = "Mapping Biases to Testing in the LLM era", LengthInMinutes = 40, RoomName = "Turing" },
        new() { Id = 3, Title = "Confessions from the brown paper - What it takes to replace legacy", LengthInMinutes = 40, RoomName = "Turing" },
        new() { Id = 4, Title = "Beyond the State Machine: The New Era of .NET Concurrency", LengthInMinutes = 40, RoomName = "Protocol" },
        new() { Id = 5, Title = "Concurrency Crossroads: Choosing between Reactive Programming and Virtual Threads", LengthInMinutes = 40, RoomName = "Turing" },
        new() { Id = 6, Title = "Architecting AI with MCP", LengthInMinutes = 40, RoomName = "Ancestors" },
        new() { Id = 7, Title = "Application Code vs Library Code", LengthInMinutes = 40, RoomName = "Roots" },
        new() { Id = 8, Title = "The best ways to use the latest OpenAPI features in .NET 11!", LengthInMinutes = 40, RoomName = "Turing" },
        new() { Id = 9, Title = "Observing Project Valhalla", LengthInMinutes = 40, RoomName = "Archive" },
        new() { Id = 10, Title = "Avoiding Déjà Vu: Building Resilient APIs with Idempotency", LengthInMinutes = 40, RoomName = "Protocol" },
        new() { Id = 11, Title = "Thinking Outside the Box: Alternative Architectures for Modern Web Applications", LengthInMinutes = 40, RoomName = "Roots" },
        new() { Id = 12, Title = "Your AI Agents Don't Know What \"Customer\" Means", LengthInMinutes = 40, RoomName = "Signal" },
        new() { Id = 13, Title = "Processing at 25: What Developers Can Learn from Creative Coding", LengthInMinutes = 40, RoomName = "Turing" },
        new() { Id = 14, Title = "When Doing Costs Nothing, Deciding Costs Everything", LengthInMinutes = 40, RoomName = "Ancestors" },
        new() { Id = 15, Title = "Building collaborative text editors with Yjs", LengthInMinutes = 40, RoomName = "Protocol" }
    ];
}
