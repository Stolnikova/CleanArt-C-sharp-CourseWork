using CleaningCrm.DTOs.Responses;

namespace CleaningCrm.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(string login, string password);
    Task<AuthResponse> RefreshAsync(string refreshToken);
}