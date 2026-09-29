using Microsoft.AspNetCore.Mvc;
using Api.Data;
using Api.Models;
using Api.Mapping;

namespace Api.Controllers;

[ApiController]
[Route("api/tools")]
public class ToolsController: ControllerBase
{
    private readonly IToolsShare _toolRepository;

    public ToolsController(IToolsShare toolRepository)
    {
        _toolRepository = toolRepository;
    }

    // GET /api/tools
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<Tool>>> GetAll()
    {
        var tools = await _toolRepository.GetToolsAsync();

        var response = tools.Select(tool => tool.ToResponse()).ToList();

        return Ok(response);
    }

    // GET /api/tools/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Tool>>GetById(Guid id)
    {
        var tool =await _toolRepository.GetToolByIdAsync(id);

        if (tool is null)
        {
            return NotFound();
        }

        return Ok(tool.ToResponse());
    }
}