using Godot;
using System;

namespace neighborhoodPizzeria.scripts;

public partial class PickUpObject : Node3D
{
	[Export] public CollisionShape3D CollisionShape;
	[Export] public RigidBody3D RigidBody;

	private Global _global;

	public override void _Ready()
	{
		_global = GetNode<Global>("/root/Global");
	}

	public void OnClick()
	{
		GD.Print("Mounted");
		_global.PickedObject = this;
		CollisionShape.Disabled = true;
	}
}
