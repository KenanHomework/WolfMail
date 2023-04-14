namespace WolfMail.DTOs.WolfMailAddressRequests;

/// <summary>
/// Represents a request object for creating a new WolfMailAddress object.
/// </summary>
public class WolfMailAddressCreateRequest
{
    /// <summary>
    /// Gets or sets the name of the WolfMailAddress.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email of the WolfMailAddress.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the user who owns the WolfMailAddress.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Gets or sets the password of the WolfMailAddress.
    /// </summary>
    public string? Password { get; set; }
}
