namespace WolfMail.DTOs.WolfMailAddressRequests;

using WolfMail.Interfaces.Requests;
using WolfMail.Models.DataModels;

/// <summary>
/// Represents a request object for update a WolfMailAddress object.
/// </summary>
public class WolfMailAddressUpdateRequest : ICanApplyUpdateRequest<WolfMailAddress>
{
    /// <summary>
    /// Gets or sets the name of the WolfMailAddress.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the email of the WolfMailAddress.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the WolfMailAddress.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Apply update request to wolf mail address
    /// </summary>
    /// <param name="targetObject"></param>
    public void Apply(ref WolfMailAddress targetObject)
    {
        if (Name is not null)
            targetObject.Name = Name;

        if (Email is not null)
            targetObject.Email = Email;

        if (Password is not null)
            targetObject.Password = Password;
    }
}
