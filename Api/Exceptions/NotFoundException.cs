namespace Api.Exceptions;

// Used when a requested Member, Tool or Loan does not exist.
public sealed class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}