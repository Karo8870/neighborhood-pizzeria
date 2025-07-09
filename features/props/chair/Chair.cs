using Godot;
using System;
using neighborhoodPizzeria.features.characters.player;

namespace neighborhoodPizzeria.features.props.chair;

/// <summary>
/// Example of a 3D object that implements hover and click callbacks.
/// </summary>
public partial class Chair : Node3D
{
	[Export]
	public string HintText
	{
		get => _sittingController.IsSitting ? string.Empty : "[Left click] to sit";
		private set { }
	}

	private Marker3D _marker;

	private SittingController _sittingController;

	public string Hint => HintText;

	public override void _Ready()
	{
		_marker = GetNode<Marker3D>("SitPosition");
		_sittingController = GetNode<SittingController>("/root/Node3D/Player/SittingController");
	}

	public void OnClick()
	{
		if (_sittingController.IsSitting)
		{
			return;
		}

		_sittingController.Sit(_marker);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!_sittingController.IsSitting || !Input.IsActionPressed("escape"))
		{
			return;
		}

		_sittingController.UnSit();
	}
}
