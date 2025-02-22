using System.Security.Claims;

namespace X.Application.Core.JWT;

public interface IJwtService
{
    string GenerateToken(IEnumerable<Claim> claims);
    ClaimsPrincipal? ValidateToken(string token);
}
