using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WolfMail.Configurations;
using WolfMail.Interfaces.Providers;
using WolfMail.Providers.RequestUser;

namespace WolfMail.Extensions;

/// <summary>
/// Provides dependency injection extension methods for configuring the application's services.
/// </summary>
public static class DI
{
    /// <summary>
    /// Adds Swagger documentation generation to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1", new OpenApiInfo { Title = "WolfMail", Version = "v1", });

            setup.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 2NG5Ff@t8ze^\""
                }
            );
            setup.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                }
            );

            var filePath = Path.Combine(AppContext.BaseDirectory, "WolfMail.xml");
            setup.IncludeXmlComments(filePath);
        });

        return services;
    }

    /// <summary>
    /// Adds configuration objects to the service collection based on values from the application's configuration file.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> instance.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddConfigs(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        /* Config Jwt  */
        var jwtConfig = new JwtConfig();
        configuration.GetSection("JWT").Bind(jwtConfig);
        services.AddSingleton(jwtConfig);

        /* Config Smtp Client */
        var smtpConfig = new SmtpClientConfig();
        configuration.GetSection("SmtpClient").Bind(smtpConfig);
        services.AddSingleton(smtpConfig);

        return services;
    }

    /// <summary>
    /// Adds authentication to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
    /// <param name="configuration">for Jwt Token</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AuthenticationAndAuthorization(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddHttpContextAccessor();
        //services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IRequestUserProvider, RequestUserProvider>();

        var jwtConfig = new JwtConfig();
        configuration.GetSection("JWT").Bind(jwtConfig);

        services
            .AddAuthentication("Bearer")
            .AddJwtBearer(
                "Bearer",
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidAudience = jwtConfig.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtConfig.Secret)
                        ),
                    };
                }
            );

        services.AddAuthorization();

        return services;
    }
}
