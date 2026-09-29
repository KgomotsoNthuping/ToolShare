using Api.DTO;
using Api.Models;

namespace Api.Mapping;

public static class ToolMapping
{
    public static ToolResponse ToResponse(this Tool tool)
    {
        return new ToolResponse(
            tool.Id,
            tool.Name,
            tool.Category,
            tool.OwnerId);
    }
}