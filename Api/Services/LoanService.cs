using Api.Data;
using Api.Models;
using Api.Exceptions;

namespace Api.Services;

public sealed class LoanService : ILoanService
{
    private readonly IToolShare _store;

    // Stores successful checkout requests using their
    // Idempotency-Key so the same request can be retried safely.
    private readonly Dictionary <string, IdempotencyRecord> _idempotencyRecords =
            new(StringComparer.Ordinal);

    // Keeps the contribution check and write together
    // while using the in-memory implementation.
    private readonly SemaphoreSlim _checkoutLock = new(1, 1);

    public LoanService(IToolShare store)
    {
        _store = store;
    }


    // GET ALL LOANS
    public async Task<IReadOnlyCollection<Loan>> GetLoansAsync()
    {
        return await _store.GetLoansAsync();
    }


    // GET LOAN BY ID
    public async Task<Loan> GetLoanByIdAsync(Guid id)
    {
        var loan = await _store.GetLoanByIdAsync(id);

        if (loan is null)
        {
            throw new NotFoundException("The loan was not found.");
        }

        return loan;
    }


    // CHECKOUT A TOOL
    public async Task<Loan>
        CheckoutAsync(
            Guid toolId,
            Guid borrowerId,
            DateTime dueDate,
            string? idempotencyKey)
    {
        // This is a defensive check.
        // Later the request validation layer will prevent
        // a missing key from reaching this method.
        if (string.IsNullOrWhiteSpace(idempotencyKey))
        {
            throw new ArgumentException("An Idempotency-Key is required.");
        }

        // Represents the payload associated with this key.
        var signature = new CheckoutSignature(
                toolId,
                borrowerId,
                dueDate);

        await _checkoutLock.WaitAsync();

        try
        {
            // IDEMPOTENCY CHECK
            // Check the key before checking whether the Tool is currently borrowed.
            if (_idempotencyRecords.TryGetValue( idempotencyKey, out var previous))
            {
                // Same key but different request.
                if (previous.Signature != signature)
                {
                    throw new ConflictException("This Idempotency-Key has already been used with a different checkout request.");
                }

                return previous.Loan;
            }


            // TOOL MUST EXIST
            var tool = await _store.GetToolByIdAsync(toolId);

            if (tool is null)
            {
                throw new NotFoundException( "The tool was not found.");
            }


            // BORROWER MUST EXIST
            var borrower = await _store.GetMemberByIdAsync(borrowerId);

            if (borrower is null)
            {
                throw new NotFoundException(
                    "The borrower was not found.");
            }


            // MAIN TOOLSHARE BUSINESS RULE:
            // A Tool cannot have more than oneactive CheckedOut Loan.
            var activeLoan =await _store.GetActiveLoanForToolAsync(toolId);

            if (activeLoan is not null)
            {
                throw new ConflictException("The tool is already checked out.");
            }


            // All checks passed, so the checkout is allowed.
            var loan =new Loan(
                    toolId,
                    borrowerId,
                    dueDate);

            await _store.AddLoanAsync(loan);


            // Store the successful request and its result.
            _idempotencyRecords[idempotencyKey] =new IdempotencyRecord(
                    signature,
                    loan);

            return loan;
        }
        finally
        {
            _checkoutLock.Release();
        }
    }


    // RETURN A TOOL
    public async Task ReturnAsync(Guid loanId)
    {
        var loan = await _store.GetLoanByIdAsync(loanId);

        if (loan is null)
        {
            throw new NotFoundException("The loan was not found.");
        }

        if (loan.Status == LoanStatus.Returned)
        {
            throw new ConflictException("The loan has already been returned.");
        }

        loan.Return();

        await _store.UpdateLoanAsync(loan);
    }


    // DELETE A LOAN
    public async Task DeleteAsync( Guid loanId)
    {
        var deleted =await _store.DeleteLoanAsync(loanId);

        if (!deleted)
        {
            throw new NotFoundException("The loan was not found.");
        }
    }


    // Used to compare a repeated request with the request that originally used the key.
    private record CheckoutSignature(
        Guid ToolId,
        Guid BorrowerId,
        DateTime DueDate);


    // Stores both the original checkout request and the Loan it created.
    private record IdempotencyRecord(
        CheckoutSignature Signature,
        Loan Loan);
}