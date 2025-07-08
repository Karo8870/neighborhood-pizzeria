using Godot;
using System;

namespace neighborhoodPizzeria;

/// <summary>
/// Example of a 3D object that implements hover and click callbacks.
/// </summary>
public partial class WoodenChair : Node3D
{
	[Export]
	public string HintText
	{
		get => _player.is_sitting ? string.Empty : "[Left click] to sit";
		private set { }
	}

	private Marker3D _marker;

	private Player _player;

	public string Hint => HintText;

	public override void _Ready()
	{
		_marker = GetNode<Marker3D>("SitPosition");
		_player = GetNode<Player>("/root/Node3D/Player");
	}

	public void OnClick()
	{
		if (!_player.is_sitting)
		{
			_player.is_sitting = true;
			_player.SetPhysicsProcess(false);
			_player.GlobalPosition = _marker.GlobalPosition;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!_player.is_sitting || !Input.IsActionPressed("escape"))
		{
			return;
		}

		_player.is_sitting = false;

		_player.SetPhysicsProcess(true);
	}
}
