using System.Text.Json.Serialization;

namespace Auth.API.DTOs.GoogleLogin
{
    public record GoogleLoginRequest(
    string IdToken,
    string Name,
    string GivenName,
    string FamilyName,
    string Email,
    string Picture,
    string Provider);
}
