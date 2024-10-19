namespace Auth.API.DTOs;

public record Token(
    string AccessToken, 
    DateTime AccessTokenExpirationDate);
