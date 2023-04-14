namespace WolfMail.Models.DataModels;

/// <summary>
/// A group of mail addresses that can be used for sending bulk messages in WolfMail.
/// </summary>
public class MailGroup
{
    /// <summary>
    /// The unique identifier of the mail group.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The name of the mail group.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A collection of <see cref="WolfMailAddress"/> objects representing the email addresses in this mail group.
    /// </summary>
    public virtual HashSet<WolfMailAddress> Mails { get; set; } = new();

    /* Navigation Properties */
    /// <summary>
    /// The identifier of the user who owns this mail group.
    /// </summary>
    public virtual User User { get; set; } = default!;

    /// <summary>
    /// Determines whether the current <see cref="MailGroup"/> instance is equal to another <see cref="MailGroup"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current instance.</param>
    /// <returns><see langword="true"/> if the objects are equal; otherwise, <see langword="false"/></returns>
    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
            return false;

        var other = obj as MailGroup;
        return Id.Equals(other!.Id);
    }

    /// <summary>
    /// Serves as the default hash function for the MailGroup class.
    /// </summary>
    /// <returns>A hash code for the current MailGroup object.</returns>
    public override int GetHashCode() => Id.GetHashCode();
}
