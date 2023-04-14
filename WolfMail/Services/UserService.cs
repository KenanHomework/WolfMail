using Microsoft.EntityFrameworkCore;
using WolfMail.Data;
using WolfMail.Interfaces.Services;
using WolfMail.Models.DataModels;

namespace WolfMail.Services;

/// <summary>
/// A Service for manage <see cref="User"/>
/// </summary>
public class UserService : IUserService
{
    private readonly WolfMailContext _context;

    /// <summary>
    /// A constructor
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public UserService(WolfMailContext context) =>
        _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc/>
    public async Task<User?> AddEmailPassword(string userId, string mailPassword)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));

        if (user is null)
            return null;

        user.MailAddress.Password = mailPassword;
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return user;
    }

    /// <inheritdoc/>
    public async Task<bool> AddUser(User user)
    {
        user.Id = IdGeneratorService.GetUniqueId();
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc/>
    public async Task<User?> GetUserById(string id, bool includeMailGroups = false) =>
        await _context.Users
            .Include(u => u.MailAddress)
            .Include(u => u.MailGroups)
            .FirstOrDefaultAsync(u => u.Id.Equals(id));

    /// <inheritdoc/>
    public async Task<User?> GetUserByEmail(string userEmail, bool includeMailGroups = false) =>
        await _context.Users
            .Include(u => u.MailAddress)
            .Include(u => u.MailGroups)
            .FirstOrDefaultAsync(u => u.MailAddress.Email.Equals(userEmail));

    /// <inheritdoc/>
    public async Task<bool> UserExistsById(string id) =>
        await _context.Users.AnyAsync(u => u.Id.Equals(id));

    /// <inheritdoc/>
    public async Task<bool> UserExistsByEmail(string email) =>
        await _context.Users.AnyAsync(u => u.MailAddress.Email.Equals(email));
}
