using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CleaningCrm.DTOs.Responses;
using CleaningCrm.Entities;
using CleaningCrm.Repositories.Interfaces;
using CleaningCrm.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CleaningCrm.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repository;
    private readonly IConfiguration _configuration;

    public AuthService(IAuthRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<AuthResponse> LoginAsync(string login, string password)
    {
        User? user = await _repository.FindByLoginAsync(login);

        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid login or password");
        }

        bool passwordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

        if (passwordValid is false)
        {
            throw new UnauthorizedAccessException("Invalid login or password");
        }

        string accessToken = GenerateAccessToken(user);
        string refreshToken = GenerateRefreshToken();

        int refreshTokenDays = int.Parse(_configuration["Jwt:RefreshTokenDays"]!);

        RefreshToken refreshTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(refreshTokenDays),
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.SaveRefreshTokenAsync(refreshTokenEntity);

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<AuthResponse> RefreshAsync(string refreshToken)
    {
        RefreshToken? tokenEntity = await _repository.FindRefreshTokenAsync(refreshToken);

        if (tokenEntity is null)
        {
            throw new UnauthorizedAccessException("Invalid refresh token");
        }

        if (tokenEntity.IsRevoked)
        {
            throw new UnauthorizedAccessException("Refresh token has been revoked");
        }

        if (tokenEntity.ExpiresAt < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Refresh token has expired");
        }

        await _repository.RevokeRefreshTokenAsync(tokenEntity);

        string newAccessToken = GenerateAccessToken(tokenEntity.User);
        string newRefreshToken = GenerateRefreshToken();

        int refreshTokenDays = int.Parse(_configuration["Jwt:RefreshTokenDays"]!);

        RefreshToken newRefreshTokenEntity = new RefreshToken
        {
            UserId = tokenEntity.User.Id,
            Token = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(refreshTokenDays),
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.SaveRefreshTokenAsync(newRefreshTokenEntity);

        return new AuthResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }

    private string GenerateAccessToken(User user)
    {
        string secret = _configuration["Jwt:Secret"]!;
        string issuer = _configuration["Jwt:Issuer"]!;
        string audience = _configuration["Jwt:Audience"]!;
        int accessTokenMinutes = int.Parse(_configuration["Jwt:AccessTokenMinutes"]!);

        SymmetricSecurityKey key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secret)
        );

        SigningCredentials credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(accessTokenMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        byte[] randomBytes = new byte[64];
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}