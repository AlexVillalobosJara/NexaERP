using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Security
{
    public interface IJwtTokenService
    {
        string GenerateToken(ClaimsPrincipal user, TimeSpan? expiration = null);
        ClaimsPrincipal ValidateToken(string token);
        string GenerateRefreshToken();
        DateTime GetTokenExpiration(string token);
        bool IsTokenExpired(string token);
    }
}
