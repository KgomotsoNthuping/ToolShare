namespace Api.Models;

public class Tool 
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Category { get; private set; }

    public Guid OwnerId { get; private set; }

    public Tool(string name,string category,Guid ownerId)
    {
        if (ownerId == Guid.Empty)
        {
            throw new ArgumentException("A tool must have an owner.");
        }

        Id = Guid.NewGuid();

        Name = ValidateName(name);
        Category = ValidateCategory(category);

        OwnerId = ownerId;
    }

    public void UpdateDetails(string name,string category)
    {
        Name = ValidateName(name);
        Category = ValidateCategory(category);
    }

    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("A tool must have a name.");
        }

        return name.Trim();
    }

    private static string ValidateCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
        {
            throw new ArgumentException"A tool must have a category.");
        }

        return category.Trim();
    }
}