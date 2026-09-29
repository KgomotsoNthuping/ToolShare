namespace Api.Models;

public class Member
{
    public Guid Id { get; private set; }

    public string FullName { get; private set; }

    public string Email { get; private set; }

    public Member(string fullName,string email)
    {
        Id = Guid.NewGuid();

        FullName = ValidateName(fullName);
        Email = ValidateEmail(email);
    }

    public void UpdateProfile(string fullName, string email)
    {
        FullName = ValidateName(fullName);
        Email = ValidateEmail(email);
    }

    private static string ValidateName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("A member must have a full name.");
        }

        return fullName.Trim();
    }

    private static string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) ||
            !email.Contains('@'))
        {
            throw new ArgumentException("A valid email address is required.");
        }

        return email.Trim();
    }
}