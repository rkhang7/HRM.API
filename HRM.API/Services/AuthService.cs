using HRM.API.Domain.Entities;
using HRM.API.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

public class AuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(ApplicationDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public string GenerateJwtToken(UserEntity user)
    {
        var jwtSettings = _config.GetSection("Jwt");

        var claims = new[]
        {
            new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString()),
            new Claim("UserId", user.Id.ToString()),
             new Claim(ClaimTypes.Name, user.UserName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public async Task<RefreshTokenEntity> SaveRefreshTokenAsync(UserEntity user)
    {
        var refreshToken = new RefreshTokenEntity
        {
            Token = GenerateRefreshToken(),
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
        };

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        return refreshToken;
    }

    // Xác thực Refresh Token và tạo Access Token mới
    public async Task<(string NewAccessToken, string NewRefreshToken)> RefreshTokenAsync(string refreshToken)
    {
        var existingToken = await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (existingToken == null || existingToken.ExpiresAt <= DateTime.UtcNow || existingToken.RevokedAt != null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }

        // Đánh dấu token cũ đã được sử dụng
        existingToken.RevokedAt = DateTime.UtcNow;
        _context.RefreshTokens.Update(existingToken);

        // Tạo token mới
        var newAccessToken = GenerateJwtToken(existingToken.User);
        var newRefreshToken = await SaveRefreshTokenAsync(existingToken.User);

        return (newAccessToken, newRefreshToken.Token);
    }

    // Thu hồi tất cả Refresh Token của user
    public async Task RevokeAllRefreshTokensAsync(int userId)
    {
        var activeTokens = await _context.RefreshTokens
            .Where(rt => rt.UserId == userId && rt.RevokedAt == null && rt.ExpiresAt > DateTime.UtcNow)
            .ToListAsync();

        foreach (var token in activeTokens)
        {
            token.RevokedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
    }



    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
