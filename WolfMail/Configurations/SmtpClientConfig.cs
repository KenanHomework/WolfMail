namespace WolfMail.Configurations;

/// <summary>
/// Configuration for an SMTP client.
/// </summary>
public class SmtpClientConfig
{
    /// <summary>
    /// The SMTP server to use.
    /// </summary>
    public string SmtpServer { get; set; } = string.Empty;

    /// <summary>
    /// The port number to use for the SMTP server.
    /// </summary>
    public int SmtpPort { get; set; }
}
