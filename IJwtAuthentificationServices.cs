using JwtApi.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace JwtApi
{
    public interface IJwtAuthentificationServices
    {
        User Authentificate(string email, string password);
        string GenerateToken(string secret, List<Claim> claims);
        
    }
}
