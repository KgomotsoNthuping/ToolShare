using Api.Models;

namespace Api.Data;

public class InMemoryToolsShare : IToolsShare
{
    private readonly List<Member> _members = [];

    private readonly List<Tool> _tools = [];

    private readonly List<Loan> _loans = [];

    public InMemoryToolsShare()
    {
        SeedData();
    }

    private void SeedData()
    {
        var testOne = new Member(
            "Test One",
            "testone@toolshare.co.za");

        var testTwo = new Member(
            "Test Two",
            "testtwo@toolshare.co.za");

        _members.Add(testOne);
        _members.Add(testTwo);


        var drill = new Tool(
            "Drill",
            "Power Tools",
            testOne.Id);

        var ladder = new Tool(
            "Ladder",
            "Ladders",
            testTwo.Id);

        _tools.Add(drill);
        _tools.Add(ladder);
    }


    public Task<IReadOnlyCollection<Member>> GetMembersAsync()
    {
        IReadOnlyCollection<Member> members =_members.AsReadOnly();

        return Task.FromResult(members);
    }

    public Task<Member?> GetMemberByIdAsync(Guid id)
    {
        var member =_members.FirstOrDefault(member => member.Id == id);

        return Task.FromResult(member);
    }

    public Task AddMemberAsync(Member member)
    {
        _members.Add(member);

        return Task.CompletedTask;
    }

    public Task UpdateMemberAsync(Member member)
    {
        var index = _members.FindIndex(existing =>existing.Id == member.Id);

        if (index >= 0)
        {
            _members[index] = member;
        }

        return Task.CompletedTask;
    }

    public Task<bool> DeleteMemberAsync(Guid id)
    {
        var member =_members.FirstOrDefault(member =>member.Id == id);

        if (member is null)
        {
            return Task.FromResult(false);
        }

        _members.Remove(member);

        return Task.FromResult(true);
    }


    public Task<IReadOnlyCollection<Tool>>GetToolsAsync()
    {
        IReadOnlyCollection<Tool> tools =_tools.AsReadOnly();

        return Task.FromResult(tools);
    }

    public Task<Tool?> GetToolByIdAsync(Guid id)
    {
        var tool = _tools.FirstOrDefault(tool =>tool.Id == id);

        return Task.FromResult(tool);
    }

    public Task AddToolAsync(Tool tool)
    {
        _tools.Add(tool);

        return Task.CompletedTask;
    }

    public Task UpdateToolAsync(Tool tool)
    {
        var index =_tools.FindIndex(existing =>existing.Id == tool.Id);

        if (index >= 0)
        {
            _tools[index] = tool;
        }

        return Task.CompletedTask;
    }

    public Task<bool> DeleteToolAsync( Guid id)
    {
        var tool =_tools.FirstOrDefault(tool =>tool.Id == id);

        if (tool is null)
        {
            return Task.FromResult(false);
        }

        _tools.Remove(tool);

        return Task.FromResult(true);
    }


    public Task<IReadOnlyCollection<Loan>> GetLoansAsync()
    {
        IReadOnlyCollection<Loan> loans = _loans.AsReadOnly();

        return Task.FromResult(loans);
    }

    public Task<Loan?> GetLoanByIdAsync(Guid id)
    {
        var loan =_loans.FirstOrDefault(loan =>loan.Id == id);

        return Task.FromResult(loan);
    }

    public Task AddLoanAsync(Loan loan)
    {
        _loans.Add(loan);

        return Task.CompletedTask;
    }

    public Task UpdateLoanAsync(Loan loan)
    {
        var index = _loans.FindIndex(existing =>existing.Id == loan.Id);

        if (index >= 0)
        {
            _loans[index] = loan;
        }

        return Task.CompletedTask;
    }

    public Task<bool> DeleteLoanAsync(Guid id)
    {
        var loan = _loans.FirstOrDefault(loan => loan.Id == id);

        if (loan is null)
        {
            return Task.FromResult(false);
        }

        _loans.Remove(loan);

        return Task.FromResult(true);
    }

    public Task<Loan?> GetActiveLoanForToolAsync(Guid toolId)
        {
            var loan =_loans.FirstOrDefault(loan =>
                loan.ToolId == toolId &&
                loan.Status == LoanStatus.CheckedOut);

            return Task.FromResult(loan);
        }
}