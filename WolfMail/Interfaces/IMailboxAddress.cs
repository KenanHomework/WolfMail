using MimeKit;

namespace WolfMail.Interfaces;

/// <summary>
/// Interface for classes that can be converted to <see cref="MailboxAddress"/> objects.
/// </summary>
public interface IMailboxAddress
{
    /// <summary>
    /// Converts the object to a <see cref="MailboxAddress"/> instance.
    /// </summary>
    /// <returns>A <see cref="MailboxAddress"/> instance.</returns>
    public MailboxAddress Convert();
}
