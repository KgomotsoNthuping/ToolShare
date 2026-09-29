namespace Api.DTO;

// Request sent when a Member checks out a Tool.
public  class CreateLoanRequest
{
    public Guid ToolId { get; init; }

    public Guid BorrowerId { get; init; }

    public DateTime DueDate { get; init; }
}

// Shape returned to clients.
public sealed record LoanResponse(
    Guid Id,
    Guid ToolId,
    Guid BorrowerId,
    DateTime CheckedOutAtUtc,
    DateTime DueDate,
    string Status,
    DateTime? ReturnedAtUtc);