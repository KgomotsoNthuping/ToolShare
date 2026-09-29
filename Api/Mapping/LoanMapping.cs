using Api.DTO;
using Api.Models;

namespace Api.Mapping;

public static class LoanMapping
{
    public static LoanResponse ToResponse(this Loan loan)
    {
        return new LoanResponse(
            loan.Id,
            loan.ToolId,
            loan.BorrowerId,
            loan.CheckedOutAtUtc,
            loan.DueDate,
            loan.Status.ToString(),
            loan.ReturnedAtUtc);
    }
}