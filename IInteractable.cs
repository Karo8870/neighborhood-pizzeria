using Godot;

namespace neighborhoodPizzeria;

/// <summary>
/// Define a contract for 3D objects that can be hovered and clicked.
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// The text to show when the player is hovering over this object.
    /// </summary>
    string Hint { get; }

    /// <summary>
    /// Called once when the player’s cursor/ray first starts hovering over this object.
    /// </summary>
    void OnHoverStart();

    /// <summary>
    /// Called once when the player’s cursor/ray stops hovering over this object.
    /// </summary>
    void OnHoverEnd();

    /// <summary>
    /// Called when the player presses the interact button (mouse down) while hovering.
    /// </summary>
    void OnClick();

    /// <summary>
    /// Called when the player releases the interact button (mouse up) while hovering.
    /// </summary>
    void OnClickEnd();
}