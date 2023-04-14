using Microsoft.EntityFrameworkCore;
using WolfMail.Data;
using WolfMail.Interfaces.Services;
using WolfMail.Models.DataModels;

namespace WolfMail.Services;

/// <summary>
/// Service for managing mail groups.
/// </summary>
public class MailGroupService : IMailGroupService
{
    private readonly WolfMailContext _context;
    private readonly IUserService _userService;

    /// <summary>
    /// A constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="userService"></param>
    public MailGroupService(WolfMailContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    /// <inheritdoc/>
    public async Task<MailGroup> AddMailGroup(string userId, MailGroup mailGroup)
    {
        var user = await _userService.GetUserById(userId) ?? throw new Exception("User not found");

        user.MailGroups ??= new HashSet<MailGroup>();
        user.MailGroups.Add(mailGroup);

        _context.Entry(user).State = EntityState.Modified;
        _context.Update(user);
        await _context.SaveChangesAsync();

        return mailGroup;
    }

    /// <inheritdoc/>
    public async Task DeleteMailGroup(string userId, string mailGroupId)
    {
        var user = await _userService.GetUserById(userId) ?? throw new Exception("User not found");

        user.MailGroups?.RemoveWhere(mg => mg.Id.Equals(mailGroupId));

        _context.Entry(user).State = EntityState.Modified;
        _context.Update(user);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<MailGroup?> GetMailGroup(string userId, string mailGroupId)
    {
        User? user = null;
        MailGroup? mailGroup = null;

        user = await _userService.GetUserById(userId) ?? throw new Exception("User not found");

        if (user.MailGroups is null)
            return null;

        mailGroup = user.MailGroups.FirstOrDefault(mg => mg.Id == mailGroupId);

        return mailGroup;
    }

    /// <inheritdoc/>
    public async Task<HashSet<MailGroup>?> GetMailGroups(string userId)
    {
        var user = await _userService.GetUserById(userId) ?? throw new Exception("User not found");
        return user.MailGroups;
    }

    /// <inheritdoc/>
    public async Task<bool> MailGroupExists(string userId, string mailGroupId)
    {
        var user = await _userService.GetUserById(userId);

        if (user is null || user.MailGroups is null)
            return false;

        return user.MailGroups.Any(mg => mg.Id.Equals(mailGroupId));
    }

    /// <inheritdoc/>
    public async Task<MailGroup?> UpdateMailGroup(
        string userId,
        string mailGroupId,
        MailGroup newMailGroup
    )
    {
        var user = await _userService.GetUserById(userId);

        if (user is null || user.MailGroups is null)
            return null;

        var oldMailGroup = user.MailGroups.FirstOrDefault(m => m.Id.Equals(mailGroupId));

        if (oldMailGroup is null)
            return null;

        // Update entity
        oldMailGroup.Name = newMailGroup.Name;
        oldMailGroup.Mails = newMailGroup.Mails;

        _context.Entry(user).State = EntityState.Modified;
        _context.Update(user);
        await _context.SaveChangesAsync();

        return newMailGroup;
    }

    /// <inheritdoc/>
    public async Task<bool> AddMail(string userId, string mailGroupId, WolfMailAddress mailAddress)
    {
        var user = await _userService.GetUserById(userId);

        if (user is null || user.MailGroups is null)
            return false;

        user.MailGroups?.First(mg => mg.Id.Equals(mailGroupId)).Mails.Add(mailAddress);

        _context.Entry(user).State = EntityState.Modified;
        _context.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> RemoveMail(string userId, string mailGroupId, string mailAddressId)
    {
        var user = await _userService.GetUserById(userId);

        if (user is null || user.MailGroups is null)
            return false;

        user.MailGroups
            ?.First(mg => mg.Id.Equals(mailGroupId))
            .Mails.RemoveWhere(mg => mg.Id.Equals(mailAddressId));

        _context.Entry(user).State = EntityState.Modified;
        _context.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
