using Godot;

namespace neighborhoodPizzeria.features.characters.player;

public partial class SittingController : Node
{
	public bool IsSitting = false;

	private CharacterBody3D _player;
	private MovementController _movementController;

	public override void _Ready()
	{
		_player = GetNode<CharacterBody3D>("/root/Node3D/Player");
		_movementController = _player.GetNode<MovementController>("MovementController");
	}

	public void Sit(Marker3D marker)
	{
		_player.GlobalPosition = marker.GlobalPosition;
		_movementController.SetPhysicsProcess(false);
		IsSitting = true;
	}

	public void UnSit()
	{
		_movementController.SetPhysicsProcess(true);
		IsSitting = false;
	}
}
