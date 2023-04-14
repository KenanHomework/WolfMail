using WolfMail.DTOs.WolfMailAddressRequests;

namespace WolfMail.DTOs.MailGroup;

/// <summary>
/// Data transfer object for creating a new MailGroup.
/// </summary>
public class MailGroupCreateRequest
{
    /// <summary>
    /// Gets or sets the name of the MailGroup.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user ID of the owner of the MailGroup.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection of WolfMailAddress objects associated with the MailGroup.
    /// </summary>
    public HashSet<WolfMailAddressCreateRequest>? Mails { get; set; }
}
