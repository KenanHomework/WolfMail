using WolfMail.Interfaces.Requests;
using WolfMail.Models.DataModels;

namespace WolfMail.DTOs.UserRequests;

/// <summary>
/// User update request DTO
/// </summary>
public class UserUpdateRequest : ICanApplyUpdateRequest<User>
{
    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string? EmailId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string? UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string? Password { get; set; } = string.Empty;

    /// <summary>
    /// Only update Username,Password
    /// </summary>
    /// <param name="targetObject"></param>
    public void Apply(ref User targetObject)
    {
        if (UserName is not null)
            targetObject.UserName = UserName;

        if (Password is not null)
            targetObject.Password = Password;
    }
}
