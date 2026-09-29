using Microsoft.AspNetCore.Mvc;
using Api.Data;
using Api.Models;
using Api.Mapping;

namespace Api.Controllers;

[ApiController]
[Route("api/members")]
public class MembersController : ControllerBase
{
    private readonly IToolsShare _memberRepository;

    public MembersController(IToolsShare memberRepository)
    {
        _memberRepository = memberRepository;
    }

    // GET /api/members
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<Member>>> GetAll()
    {
        var members = await _memberRepository.GetAllAsync();

        var response = members.Select(member => member.ToResponse()).ToList();

        return Ok(response);
    }

    // GET /api/members/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Member>>GetById(Guid id)
    {
        var member = await _memberRepository.GetByIdAsync(id);

        if (member is null)
        {
            return NotFound();
        }

        return Ok(member.ToResponse());
    }
}