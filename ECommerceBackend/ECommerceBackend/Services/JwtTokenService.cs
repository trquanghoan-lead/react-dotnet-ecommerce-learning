using ECommerceBackend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceBackend.Services;

public sealed class JwtTokenService(IConfiguration configuration)
{
    private const int TokenLifetimeMinutes = 60;

    public LoginResponse CreateToken(Account account, int cartId)
    {
        var key = configuration["Jwt:Key"];
        var issuer = configuration["Jwt:Issuer"] ?? "ECommerceBackend";

        if (string.IsNullOrWhiteSpace(key) || key.Length < 32)
            throw new InvalidOperationException("Jwt:Key must contain at least 32 characters.");

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, account.UserId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, account.UserId.ToString()),
            new Claim(ClaimTypes.Name, account.Username),
            new Claim(ClaimTypes.Role, account.Role)
        };

        var token = new JwtSecurityToken(
            issuer,
            issuer,
            claims,
            expires: DateTime.UtcNow.AddMinutes(TokenLifetimeMinutes),
            signingCredentials: credentials);

        return new LoginResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresIn = TokenLifetimeMinutes * 60,
            UserId = account.UserId,
            CartId = cartId,
            Username = account.Username,
            DisplayName = account.DisplayName
        };
    }
}
