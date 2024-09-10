namespace ConfigurableWarning.API.State;

/// <summary>
/// An object that has a <see cref="StateHolder{T}" />.
/// </summary>
/// <typeparam name="T">The type of the state that is held.</typeparam>
public interface IHasStateHolder<T> {
    /// <summary>
    /// Get this object's state holder.
    /// </summary>
    /// <returns>The state holder.</returns>
    StateHolder<T> StateHolder { get; }
}
