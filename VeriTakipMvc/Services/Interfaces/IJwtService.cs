using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace VeriTakipMvc.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateJwtToken(string userName, string secretKey);
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
