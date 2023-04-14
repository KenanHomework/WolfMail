namespace WolfMail.Models.DataModels;

/// <summary>
/// Represents a user of the WolfMail application.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the unique identifier of the user
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /* Navigation Properties */

    /// <summary>
    /// Gets or sets the mail groups that the user has to, if any.
    /// </summary>
    public virtual HashSet<MailGroup>? MailGroups { get; set; }

    /// <summary>
    /// Gets or sets of the user's associated mail address.
    /// </summary>
    public virtual WolfMailAddress MailAddress { get; set; } = default!;

    /// <summary>
    /// Determines whether the current <see cref="User"/> instance is equal to another <see cref="User"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current instance.</param>
    /// <returns><see langword="true"/> if the objects are equal; otherwise, <see langword="false"/></returns>
    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
            return false;

        var other = obj as User;
        return Id.Equals(other!.Id);
    }

    /// <summary>
    /// Serves as the default hash function for the MailGroup class.
    /// </summary>
    /// <returns>A hash code for the current MailGroup object.</returns>
    public override int GetHashCode() => Id.GetHashCode();
}
