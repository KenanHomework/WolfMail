namespace WolfMail.Interfaces.Requests;

/// <summary>
/// Represents an interface for applying update requests to an object.
/// </summary>
public interface ICanApplyUpdateRequest<T>
{
    /// <summary>
    /// Applies an update request to a target object.
    /// </summary>
    /// <typeparam name="T">The type of the target object.</typeparam>
    /// <param name="targetObject">The target object to apply the update request to.</param>
    void Apply(ref T targetObject);
}
