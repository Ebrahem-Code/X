namespace X.API.Contracts.Users;

public sealed record UpdateUserRequest(
    string FirstName,
    string LastName, 
    string Email, 
    string Password);
