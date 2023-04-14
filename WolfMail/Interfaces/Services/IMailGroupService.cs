using WolfMail.Models.DataModels;

namespace WolfMail.Interfaces.Services;

/// <summary>
/// Interface for managing mail groups.
/// </summary>
public interface IMailGroupService
{
    /// <summary>
    /// Retrieves a collection of mail groups associated with a user identified by their user ID.
    /// </summary>
    /// <param name="userId">The ID of the user to retrieve mail groups for.</param>
    /// <returns>A task representing the asynchronous operation that returns an <see cref="HashSet{T}"/> of <see cref="MailGroup"/> objects associated with the user identified by the provided user ID. If the user is not found or does not have any mail groups, the method returns <see langword="null"/>.</returns>
    Task<HashSet<MailGroup>?> GetMailGroups(string userId);

    /// <summary>
    /// Get a <see cref="MailGroup"/> by ID.
    /// </summary>
    /// <param name="userId">The ID of the  <see cref="User"/> to retrieve.</param>
    /// <param name="mailGroupId">The ID of the <see cref="MailGroup"/> to retrieve.</param>
    /// <returns>The <see cref="MailGroup"/> with the given ID, or <see langword="null"/> if it does not exist.</returns>
    public Task<MailGroup?> GetMailGroup(string userId, string mailGroupId);

    /// <summary>
    /// Add a new <see cref="MailGroup"/>.
    /// </summary>
    /// <param name="userId">The ID of the  <see cref="User"/> to retrieve.</param>
    /// <param name="mailGroup">The <see cref="MailGroup"/> to add.</param>
    /// <returns>The added <see cref="MailGroup"/>.</returns>
    public Task<MailGroup> AddMailGroup(string userId, MailGroup mailGroup);

    /// <summary>
    /// Update an existing <see cref="MailGroup"/>.
    /// </summary>
    /// <param name="userId">The ID of the  <see cref="User"/> to retrieve.</param>
    /// <param name="mailGroupId">The ID of the <see cref="MailGroup"/> to update.</param>
    /// <param name="newMailGroup">The updated <see cref="MailGroup"/>.</param>
    /// <returns>The updated <see cref="MailGroup"/>.</returns>
    public Task<MailGroup?> UpdateMailGroup(
        string userId,
        string mailGroupId,
        MailGroup newMailGroup
    );

    /// <summary>
    /// Delete a <see cref="MailGroup"/> by ID.
    /// </summary>
    /// <param name="userId">The ID of the  <see cref="User"/> to retrieve.</param>
    /// <param name="mailGroupId">The ID of the <see cref="MailGroup"/> to delete.</param>
    public Task DeleteMailGroup(string userId, string mailGroupId);

    /// <summary>
    /// Check if a <see cref="MailGroup"/> exists.
    /// </summary>
    /// <param name="userId">The ID of the  <see cref="User"/> to retrieve.</param>
    /// <param name="mailGroupId">The ID of the <see cref="MailGroup"/> to check.</param>
    /// <returns><see langword="true"/> if the <see cref="MailGroup"/> exists, <see langword="false"/> otherwise.</returns>
    public Task<bool> MailGroupExists(string userId, string mailGroupId);

    /// <summary>
    /// Adds mail to an existing mail group
    /// </summary>
    /// <param name="userId">The ID of the  <see cref="User"/> to retrieve.</param>
    /// <param name="mailGroupId"></param>
    /// <param name="mailAddress"></param>
    /// <returns><see langword="true"/> if successfully added, otherwise <see langword="false"/></returns>
    public Task<bool> AddMail(string userId, string mailGroupId, WolfMailAddress mailAddress);

    /// <summary>
    /// Remove mail from an existing mail group
    /// </summary>
    /// <param name="userId">The ID of the  <see cref="User"/> to retrieve.</param>
    /// <param name="mailGroupId"></param>
    /// <param name="mailAddressId"></param>
    /// <returns><see langword="true"/> if successfully removed, otherwise <see langword="false"/></returns>
    public Task<bool> RemoveMail(string userId, string mailGroupId, string mailAddressId);
}
