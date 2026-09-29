namespace Api.Models;

public class Loan 
{
    public Guid Id { get; private set; }

    public Guid ToolId { get; private set; }

    public Guid BorrowerId { get; private set; }

    public DateTime CheckedOutAtUtc { get; private set; }

    public DateTime DueDate { get; private set; }

    // public LoanStatus Status { get; private set; }

    public DateTime? ReturnedAtUtc { get; private set; }

    public Loan(Guid toolId,Guid borrowerId,DateTime dueDate)
    {
        if (toolId == Guid.Empty)
        {
            throw new ArgumentException("A tool is required.");
        }

        if (borrowerId == Guid.Empty)
        {
            throw new ArgumentException("A borrower is required.");
        }

        Id = Guid.NewGuid();

        ToolId = toolId;
        BorrowerId = borrowerId;
        CheckedOutAtUtc = DateTime.UtcNow;
        DueDate = dueDate;
        Status = LoanStatus.CheckedOut;
    }

    public void Return()
    {
        if (Status == LoanStatus.Returned)
        {
            throw new InvalidOperationException("The loan has already been returned.");
        }

        Status = LoanStatus.Returned;
        ReturnedAtUtc = DateTime.UtcNow;
    }
}