using Api.Models

namespace Api.Services;

public interface ILoanService
{
    Task<IReadOnlyCollection<Loan>> GetLoansAsync();

    Task<Loan> GetLoanByIdAsync(Guid id);

    Task<Loan> CheckoutAsync(
        Guid toolId,
        Guid borrowerId,
        DateTime dueDate,
        string? idempotencyKey);

    Task ReturnAsync(Guid loanId);

    Task DeleteAsync(Guid loanId);
}