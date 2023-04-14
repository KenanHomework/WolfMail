using WolfMail.Providers.RequestUser;

namespace WolfMail.Interfaces.Providers;

/// <summary>
/// Provider for retrieving information about the current user making the request.
/// </summary>
public interface IRequestUserProvider
{
    /// <summary>
    /// Gets the user information for the current request, if available.
    /// </summary>
    /// <returns>The <see cref="UserInfo"/>, or <see langword="null"/> if no user information is available.</returns>
    UserInfo? GetUserInfo();
}
