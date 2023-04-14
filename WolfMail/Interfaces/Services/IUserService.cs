using WolfMail.Models.DataModels;

namespace WolfMail.Interfaces.Services;

/// <summary>
/// Interface for user service
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Retrieves a <see cref="User"/> by their ID
    /// </summary>
    /// <param name="id">The ID of the <see cref="User"/>  to retrieve</param>
    /// <param name="includeMailGroups">Whether to include the user's mail groups in the result.</param>
    /// <returns>The retrieved <see cref="User"/>  or <see langword="null"/> if the <see cref="User"/> does not exist</returns>
    public Task<User?> GetUserById(string id, bool includeMailGroups = false);

    /// <summary>
    /// Retrieves a <see cref="User"/> by their email
    /// </summary>
    /// <param name="userEmail">The email of the <see cref="User"/> to retrieve</param>
    /// <param name="includeMailGroups">Whether to include the user's mail groups in the result.</param>
    /// <returns>The retrieved <see cref="User"/> or <see langword="null"/> if the <see cref="User"/> does not exist</returns>
    public Task<User?> GetUserByEmail(string userEmail, bool includeMailGroups = false);

    /// <summary>
    /// Adds a new <see cref="User"/>
    /// </summary>
    /// <param name="user">The <see cref="User"/> to add</param>
    /// <returns><see langword="true"/> if the <see cref="User"/> was added successfully, <see langword="false"/> otherwise</returns>
    public Task<bool> AddUser(User user);

    /// <summary>
    /// Adds a password to user's <see cref="WolfMailAddress"/>.
    /// <para>The <see cref="WolfMailAddress.Password"/> only using for sending emails as author
    /// .Otherwise you don't need  <see cref="WolfMailAddress.Password"/></para>
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="mailPassword"></param>
    /// <returns>The updated <see cref="User"/></returns>
    public Task<User?> AddEmailPassword(string userId, string mailPassword);

    /// <summary>
    /// Checks if a <see cref="User"/> with the given ID exists
    /// </summary>
    /// <param name="id">The ID of the <see cref="User"/> to check</param>
    /// <returns><see langword="true"/> if the <see cref="User"/> exists, <see langword="false"/> otherwise</returns>
    public Task<bool> UserExistsById(string id);

    /// <summary>
    /// Checks if a <see cref="User"/> with the given email exists
    /// </summary>
    /// <param name="email">The email of the <see cref="User"/> to check</param>
    /// <returns><see langword="true"/> if the <see cref="User"/> exists, <see langword="false"/> otherwise</returns>
    public Task<bool> UserExistsByEmail(string email);
}
