using MimeKit;
using WolfMail.Interfaces;

namespace WolfMail.Models.DataModels;

/// <summary>
/// This class is a mailing address class for use in WolfMail.
/// </summary>
public class WolfMailAddress : IMailboxAddress
{
    /// <summary>
    /// Gets or sets the unique identifier of the MailAddress
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The id of the linked <see cref="User"/> element
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Display name of the e-mail
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// E-mail address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// E-mail password
    /// This password is required to send messages, if you will not send messages with this e-mail address, you can skip this feature.
    /// </summary>
    public string? Password { get; set; }

    /*  Navigation Properties */

    /// <summary>
    /// Navigation property of  <see cref="User"/> element
    /// </summary>
    public virtual User? User { get; set; }

    /// <inheritdoc/>
    public virtual MailboxAddress Convert() => new(Name, Email);

    /// <summary>
    /// Determines whether the current <see cref="WolfMailAddress"/> instance is equal to another <see cref="WolfMailAddress"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current instance.</param>
    /// <returns><see langword="true"/> if the objects are equal; otherwise, <see langword="false"/></returns>
    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
            return false;

        var other = obj as WolfMailAddress;
        return Id.Equals(other!.Id);
    }

    /// <summary>
    /// Serves as the default hash function for the MailGroup class.
    /// </summary>
    /// <returns>A hash code for the current MailGroup object.</returns>
    public override int GetHashCode() => Id.GetHashCode();
}
