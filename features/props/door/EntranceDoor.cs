using Godot;
using System;

namespace neighborhoodPizzeria;

public partial class EntranceDoor : StaticBody3D
{
	private bool is_open;

	[Export]
	public string HintText
	{
		get => is_open ? "Close door" : "Open door";
		private set { }
	}

	public void OnClick()
	{
		var owner = GetOwner() as Node3D;

		is_open = !is_open;

		owner.Rotation = new Vector3(owner.Rotation.X, is_open ? float.DegreesToRadians(90) : float.DegreesToRadians(0),
			owner.Rotation.Z);

		owner.GetNode<AnimationPlayer>("AnimationPlayer").Play(is_open ? "open" : "close");
	}
}
