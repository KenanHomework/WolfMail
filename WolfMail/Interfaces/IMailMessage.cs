using MimeKit;

namespace WolfMail.Interfaces;

/// <summary>
/// Interface for classes that can be converted to <see cref="MimeMessage"/> objects.
/// </summary>
public interface IMailMessage
{
    /// <summary>
    /// Converts the object to a <see cref="MimeMessage"/>.
    /// </summary>
    /// <returns>The converted <see cref="MimeMessage"/>.</returns>
    public MimeMessage Convert();
}
