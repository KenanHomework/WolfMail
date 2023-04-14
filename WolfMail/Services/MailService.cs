using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using WolfMail.Interfaces;
using WolfMail.Interfaces.Services;
using WolfMail.Configurations;

namespace WolfMail.Services;

/// <summary>
/// A service responsible for sending email messages
/// </summary>
public class MailService : IMailService
{
    private readonly SmtpClientConfig _smtpClientConfig;

    /// <summary>
    /// A constructor
    /// </summary>
    /// <param name="smtpClientConfig"></param>
    public MailService(SmtpClientConfig smtpClientConfig) =>
        _smtpClientConfig =
            smtpClientConfig ?? throw new ArgumentNullException(nameof(smtpClientConfig));

    /// <inheritdoc/>
    public void SendMail(string senderUsername, string senderPassword, IMailMessage message) =>
        SendMail(senderUsername, senderPassword, message.Convert());

    /// <inheritdoc/>
    public async void SendMail(string senderUsername, string senderPassword, MimeMessage message)
    {
        using var client = new SmtpClient();
        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
        await client.ConnectAsync(
            _smtpClientConfig.SmtpServer,
            _smtpClientConfig.SmtpPort,
            SecureSocketOptions.StartTls
        );
        await client.AuthenticateAsync(senderUsername, senderPassword);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
