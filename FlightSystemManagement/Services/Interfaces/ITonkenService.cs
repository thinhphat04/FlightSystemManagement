using System.Security.Claims;
using FlightSystemManagement.Entity;

namespace FlightSystemManagement.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateAccessToken(User user);
        string CreateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}