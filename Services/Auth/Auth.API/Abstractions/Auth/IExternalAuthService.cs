using Auth.API.DTOs.GoogleLogin;

namespace Auth.API.Abstractions.Auth;

public interface IExternalAuthService
{
    Task<GoogleLoginRequest> GoogleLogin(GoogleLoginResponse request, CancellationToken cancellationToken);
}
