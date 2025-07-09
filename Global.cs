using Godot;
using neighborhoodPizzeria.scripts;

namespace neighborhoodPizzeria;

public partial class Global : Node
{
	[Export] public bool CanMove { get; set; } = true;

	[Export] public PickUpObject PickedObject { get; set; }
}
