using Auth.API.DTOs.LoginUser;

namespace Auth.API.Abstractions.Auth
{
    public interface IInternalAuthService
    {
        Task<LoginUserResponse> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken);
    }
}
