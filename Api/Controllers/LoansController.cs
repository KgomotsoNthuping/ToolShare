using Api.DTO;
using Api.Mapping;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/loans")]
public sealed class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }


    // GET /api/loans
    [HttpGet]
    public async Task<
        ActionResult<IReadOnlyCollection<LoanResponse>>>GetAll()
    {
        var loans =await _loanService.GetLoansAsync();

        var response = loans.Select(loan =>loan.ToResponse()).ToList();

        return Ok(response);
    }


    // GET /api/loans/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LoanResponse>>GetById(Guid id)
    {
        var loan =await _loanService.GetLoanByIdAsync(id);

        return Ok(loan.ToResponse());
    }


    // POST /api/loans
    // Checks out a Tool to a Member.
    [HttpPost]
    public async Task<ActionResult<LoanResponse>>
        Checkout([FromBody] CreateLoanRequest request)
    {
        var loan = await _loanService.CheckoutAsync(
                request.ToolId,
                request.BorrowerId,
                request.DueDate,
                idempotencyKey);

        var response = loan.ToResponse();

        return CreatedAtAction(nameof(GetById), new { id = loan.Id }, response);
    }


    // POST /api/loans/{id}/return
    // Marks an active Loan as Returned.
    [HttpPost("{id:guid}/return")]
    public async Task<IActionResult> Return(Guid id)
    {
        await _loanService.ReturnAsync(id);

        return NoContent();
    }


    // DELETE /api/loans/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult>Delete(Guid id)
    {
        await _loanService.DeleteAsync(id);

        return NoContent();
    }
}