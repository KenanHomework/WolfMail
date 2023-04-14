namespace WolfMail.DTOs.UserRequests;

/// <summary>
/// User create request DTO
/// </summary>
public class UserCreateRequest
{
    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password of the user's email.
    /// </summary>
    public string? EmailPassword { get; set; }
}
