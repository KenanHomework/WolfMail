using MimeKit;
using WolfMail.Interfaces;
using WolfMail.Models.DataModels;

namespace WolfMail.Models;

/// <summary>
/// Represents a message in WolfMail. A message consists of a <see cref="Subject"/>, a <see cref="Body"/>, a list of <see cref="To"/> addresses, and an optional <see cref="Group"/> for sending bulk messages. Additionally, the message contains endpoint information.
/// <list type="bullet">
///     <item>
///         <see cref="From"/> - The sender of the message.
///     </item>
///     <item>
///         <see cref="To"/> or <see cref="Group"/> - The recipient(s) of the message.
///     </item>
/// </list>
/// </summary>
public class WolfMailMessage : IMailMessage
{
    /// <summary>
    /// Gets or sets the sender of the message.
    /// </summary>
    public WolfMailAddress From { get; set; } = new WolfMailAddress();

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
    public MailGroup Group { get; set; } = new MailGroup();

    /// <summary>
    /// Gets or sets the list of addresses to send the message to.
    /// If a group is specified, e-mail will be sent to additionally selected addresses.
    /// </summary>
    public IEnumerable<string> To { get; set; } = Enumerable.Empty<string>();

    /// <inheritdoc/>
    public MimeMessage Convert()
    {
        MimeMessage message =
            new()
            {
                /* Message Data */
                Subject = Subject,
                Body = new TextPart("plain") { Text = Body }
            };

        /* Endpoints */
        // Author
        message.From.Add(From.Convert());

        // Receiver(s)
        foreach (var address in To)
            message.To.Add(new MailboxAddress("", address));

        foreach (var address in Group.Mails)
            message.To.Add(address.Convert());

        return message;
    }
}
