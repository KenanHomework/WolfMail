using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WolfMail.Data;
using WolfMail.DTOs.Auth;
using WolfMail.Interfaces.Services;
using WolfMail.Models.DataModels;
using WolfMail.Services;

namespace WolfMail.Controllers;

/// <summary>
/// Controller for handling user authentication
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly WolfMailContext _context;
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="jwtService">The JWT service instance used for generating access tokens.</param>
    /// <param name="context">The Database Context used for access DB</param>
    /// <param name="userService"></param>
    public AuthController(IJwtService jwtService, WolfMailContext context, IUserService userService)
    {
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    /// <summary>
    /// Endpoint for registering a new user
    /// </summary>
    /// <param name="request">The registration request containing user email and password</param>
    /// <returns>The generated access token for the new user</returns>
    [HttpPost("register")]
    public async Task<ActionResult<AuthTokenDto>> Register([FromBody] RegisterRequest request)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(
            u => u.MailAddress.Email == request.Email
        );

        if (existingUser is not null)
            return Conflict("User already exists");

        var user = new User()
        {
            Id = IdGeneratorService.GetUniqueId(),
            UserName = request.Name,
            Password = request.Password,
            MailAddress = new()
            {
                Id = IdGeneratorService.GetUniqueId(),
                Email = request.Email,
                Name = request.Name,
                Password = request.EmailPassword
            },
        };

        await _userService.AddUser(user);

        var accessToken = _jwtService.GenerateSecurityToken(user.Id, request.Email);
        return new AuthTokenDto { AccessToken = accessToken };
    }

    /// <summary>
    /// Endpoint for user login.
    /// </summary>
    /// <param name="request">The login request containing user email and password</param>
    /// <returns>The generated access token for the logged in user</returns>
    [HttpPost("login")]
    public async Task<ActionResult<AuthTokenDto>> Login([FromBody] LoginRequest request)
    {
        // Get user
        var user = await _context.Users.FirstOrDefaultAsync(
            u => u.MailAddress.Email == request.Email
        );

        if (user is null)
            return NotFound();

        if (!user.Password.Equals(request.Password))
            return Unauthorized();

        var accessToken = _jwtService.GenerateSecurityToken(user.Id, request.Email);
        return new AuthTokenDto { AccessToken = accessToken };
    }
}
