using izBackend.Models.Common.Auth;
using izBackend.Models.Requests;
using System.Security.Claims;

namespace izBackend.Services.Auth
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(TokenRequest request, out AuthResponse token);
        string GetUserID(ClaimsPrincipal User);
        bool RenewToken(RefreshTokenRequest request, out AuthResponse res);
    }
}
