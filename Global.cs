using Godot;

namespace neighborhoodPizzeria;

public partial class Global : Node
{
	[Export] public bool CanMove { get; set; } = true;

	[Export] public Node3D PickedObject { get; set; }
}
