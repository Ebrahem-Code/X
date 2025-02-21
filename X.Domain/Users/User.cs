namespace X.Domain.Users;

public sealed class User 
{
    private User(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    private User() { }

    public Guid Id { get; private set; } = default!;    
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Password { get; private set; } = default!;

    public static User Create(string firstName, string lastName, string email, string password)
    {
        return new User(firstName, lastName, email, password);
    }

    public void UpdateName(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public void UpdateEmail(string email)
    {
        Email = email;
    }

    public void UpdatePassword(string password)
    {
        Password = password;
    }
}
