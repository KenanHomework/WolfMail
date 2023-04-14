using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using WolfMail.Data;
using WolfMail.DTOs.UserRequests;
using WolfMail.Extensions;
using WolfMail.Interfaces.Providers;
using WolfMail.Interfaces.Services;
using WolfMail.Models;
using WolfMail.Models.DataModels;
using WolfMail.Services;

namespace WolfMail.Controllers;

/// <summary>
/// Controller for handling user management
/// </summary>
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly WolfMailContext _context;
    private readonly IUserService _userService;
    private readonly IMailGroupService _mailGroupService;
    private readonly IMailService _mailService;
    private readonly IRequestUserProvider _provider;

    /// <summary>
    /// A constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="userService"></param>
    /// <param name="mailGroupService"></param>
    /// <param name="provider"></param>
    /// <param name="mailService"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public UsersController(
        WolfMailContext context,
        IUserService userService,
        IMailGroupService mailGroupService,
        IRequestUserProvider provider,
        IMailService mailService
    )
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _mailGroupService = mailGroupService;
        _provider = provider;
        _mailService = mailService;
    }

    /// <summary>
    /// Get a user by id
    /// </summary>
    /// <param name="id">The id of the user to get</param>
    /// <returns>The requested user</returns>
    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(string id)
    {
        if (_context.Users == null)
            return NotFound();

        var user = await _userService.GetUserById(id);

        if (user is null)
            return NotFound();

        return this.SerializeIntoContent(user);
    }

    /// <summary>
    /// Update a user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request">The request</param>
    /// <returns>A result indicating success or failure</returns>
    // PUT: api/Users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(string id, UserUpdateRequest request)
    {
        var user = await _userService.GetUserById(id);

        if (user is null)
            return NotFound("User not found");

        var targetEmailAddress = await _context.MailAddresses.FirstAsync(
            m => m.Id.Equals(request.EmailId)
        );

        if (targetEmailAddress.UserId is not null)
            return Problem("The target email address is already linked a user !");

        user.MailAddress = targetEmailAddress;

        request.Apply(ref user);

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
                return NotFound();
            else
                throw;
        }

        return Ok();
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="request">The user dto object to create</param>
    /// <returns>The newly created user</returns>
    // POST: api/Users
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(UserCreateRequest request)
    {
        if (_context.Users == null)
            return Problem("Entity set 'WolfMailContext.Users'  is null.");

        User user =
            new()
            {
                Id = IdGeneratorService.GetUniqueId(),
                UserName = request.UserName,
                Password = request.Password,
                MailAddress = new WolfMailAddress()
                {
                    Id = IdGeneratorService.GetUniqueId(),
                    Name = request.UserName,
                    Email = request.Email,
                    Password = request.Password,
                }
            };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return this.SerializeIntoContent(user);
    }

    /// <summary>
    /// Delete a user by id
    /// </summary>
    /// <param name="id">The id of the user to delete</param>
    /// <returns>A result indicating success or failure</returns>
    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        if (_context.Users == null)
            return NotFound();

        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    [HttpPost("SendMail")]
    public async Task<IActionResult> SendMessage(MessageCreateRequest request)
    {
        WolfMailMessage message = new();

        // From
        var from = await _context.MailAddresses.FirstOrDefaultAsync(
            m => m.Id.Equals(request.FromMailId)
        );
        if (from is null)
            return NotFound("From Mail Address Not Found !");

        if (from.Password is null)
            return BadRequest("From password can't be null!");

        message.From = from;

        // Group
        if (request.GroupId is not null)
        {
            string? userId = _provider.GetUserInfo()!.Id;
            var group = await _mailGroupService.GetMailGroup(userId, request.GroupId!);
            if (group is null)
                return NotFound("Group not found");
            message.Group = group;
        }

        message.Subject = request.Subject;
        message.Body = request.Body;
        message.To = request.To;

        _mailService.SendMail(from.Name, from.Password, message);

        return Ok();
    }

    private bool UserExists(string id) =>
        (_context.Users?.Any(e => e.Id.Equals(id))).GetValueOrDefault();
}
