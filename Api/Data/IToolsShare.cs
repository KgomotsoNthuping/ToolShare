using Api.Models;

namespace Api.Data;

public interface IToolsShare
{
    Task<IReadOnlyCollection<Member>> GetMembersAsync();

    Task<Member?> GetMemberByIdAsync(Guid id);

    Task AddMemberAsync(Member member);

    Task UpdateMemberAsync(Member member);

    Task<bool> DeleteMemberAsync(Guid id);


    Task<IReadOnlyCollection<Tool>> GetToolsAsync();

    Task<Tool?> GetToolByIdAsync(Guid id);

    Task AddToolAsync(Tool tool);

    Task UpdateToolAsync(Tool tool);

    Task<bool> DeleteToolAsync(Guid id);


    Task<IReadOnlyCollection<Loan>> GetLoansAsync();

    Task<Loan?> GetLoanByIdAsync(Guid id);

    Task AddLoanAsync(Loan loan);

    Task UpdateLoanAsync(Loan loan);

    Task<bool> DeleteLoanAsync(Guid id);

    Task<Loan?> GetActiveLoanForToolAsync(Guid toolId);
}