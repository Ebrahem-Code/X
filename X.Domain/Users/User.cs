using X.Domain.Core.BaseEntity;

namespace X.Domain.Users;

public sealed class User : AggregateRoot
{
    private User(string firstName, string lastName, string email, string password)
        : base(Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    private User() { }

    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Password { get; private set; } = default!;

    public static User Create(string firstName, string lastName, string email, string password)
    {
        return new User(firstName, lastName, email, password);
    }

    public void Update(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
