using Microsoft.EntityFrameworkCore;
using WolfMail.Data;
using WolfMail.Models;
using WolfMail.Models.DataModels;

namespace WolfMail.DTOs.UserRequests;

/// <summary>
/// WolfMailMessage creation DTO
/// </summary>
public class MessageCreateRequest
{
    /// <summary>
    /// Gets or sets the sender of the message.
    /// </summary>
    public string FromMailId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the subject of the message. The subject is used as the title of the <see cref="WolfMailMessage"/> and is shown at the top.
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the body of the message. The body can be plain text and is the main content of the <see cref="WolfMailMessage"/>.
    /// </summary>
    public string Body { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the group to send the message to. A group can be used to send bulk messages to multiple recipients.
    /// </summary>
    public string? GroupId { get; set; }

    /// <summary>
    /// Gets or sets the list of addresses to send the message to.
    /// If a group is specified, e-mail will be sent to additionally selected addresses.
    /// </summary>
    public IEnumerable<string> To { get; set; } = Enumerable.Empty<string>();
}
