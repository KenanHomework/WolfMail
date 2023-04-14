using Microsoft.EntityFrameworkCore;
using WolfMail.Data;
using WolfMail.Extensions;
using WolfMail.Interfaces.Services;
using WolfMail.Services;

namespace WolfMail;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<WolfMailContext>(
            options => options.UseSqlite("Data Source=WolfMailDB.db")
        );

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddScoped<IMailService, MailService>();
        builder.Services.AddSingleton<IJwtService, JwtService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IMailGroupService, MailGroupService>();
        builder.Services.AddConfigs(builder.Configuration);
        builder.Services.AddSwagger();
        builder.Services.AuthenticationAndAuthorization(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
