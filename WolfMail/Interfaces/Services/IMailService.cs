using MimeKit;

namespace WolfMail.Interfaces.Services;

/// <summary>
/// Interface for a mail service responsible for sending email messages.
/// </summary>
public interface IMailService
{
    /// <summary>
    /// Sends an email message by converting an <see cref="IMailMessage"/> object to a <see cref="MimeMessage"/> object.
    /// </summary>
    /// <param name="senderUsername">The username of the sender's email account.</param>
    /// <param name="senderPassword">The password of the sender's email account.</param>
    /// <param name="message">The mail message to send.</param>
    void SendMail(string senderUsername, string senderPassword, IMailMessage message);

    /// <summary>
    /// Sends an email message.
    /// </summary>
    /// <param name="senderUsername">The username of the sender's email account.</param>
    /// <param name="senderPassword">The password of the sender's email account.</param>
    /// <param name="message">The <see cref="MimeMessage"/> to send.</param>
    void SendMail(string senderUsername, string senderPassword, MimeMessage message);
}
