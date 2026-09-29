namespace Api.DTO;

public record ToolResponse(
    Guid Id,
    string Name,
    string Category,
    Guid OwnerId);