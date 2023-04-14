﻿using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WolfMail.Configurations;
using WolfMail.Interfaces.Services;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WolfMail.Services;

/// <summary>
/// A JWT service responsible for generating a security token for a given email address.
/// </summary>
public class JwtService : IJwtService
{
    private readonly JwtConfig _config;

    /// <summary>
    /// A constructor
    /// </summary>
    /// <param name="config"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public JwtService(JwtConfig config) =>
        _config = config ?? throw new ArgumentNullException(nameof(config));

    /// <inheritdoc/>
    public string GenerateSecurityToken(string id, string email)
    {
        var claims = new[] { new Claim("user_id", id), new Claim("user_email", email) };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config.Issuer,
            audience: _config.Audience,
            expires: DateTime.UtcNow.AddMinutes(_config.ExpiresInMinutes),
            signingCredentials: signingCredentials,
            claims: claims
        );
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return accessToken;
    }
}
