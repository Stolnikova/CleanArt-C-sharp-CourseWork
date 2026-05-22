using CleaningCrm.Entities;

namespace CleaningCrm.Repositories.Interfaces;

public interface IAuthRepository
{
    Task<User?> FindByLoginAsync(string login);
    Task SaveRefreshTokenAsync(RefreshToken refreshToken);
    Task<RefreshToken?> FindRefreshTokenAsync(string token);
    Task RevokeRefreshTokenAsync(RefreshToken refreshToken);
}