namespace WolfMail.DTOs.MailGroupRequests;

/// <summary>
/// Represents a request object for adding a mail to a mail group.
/// </summary>
public class MailGroupAddMailRequest
{
    /// <summary>
    /// Gets or sets the ID of the user who is adding the mail.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the mail group to which the mail is being added.
    /// </summary>
    public string MailGroupId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the mail address to be added.
    /// </summary>
    public string MailAddressId { get; set; } = string.Empty;
}
