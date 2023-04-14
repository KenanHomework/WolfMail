using WolfMail.Models.DataModels;

namespace WolfMail.DTOs.MailGroup;

/// <summary>
/// Represents a request object for updating a mail group.
/// </summary>
public class MailGroupUpdateRequest
{
    /// <summary>
    /// Gets or sets the ID of the user who is updating the mail group.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the mail group.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the set of WolfMailAddress objects for the mail group.
    /// </summary>
    public HashSet<WolfMailAddress>? Mails { get; set; }
}
