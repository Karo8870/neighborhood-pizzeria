using Godot;
using System;

namespace neighborhoodPizzeria;

public partial class PickUpObject : RigidBody3D
{
	private Global _global;
	public CollisionShape3D CollisionShape;

	public override void _Ready()
	{
		_global = GetNode<Global>("/root/Global");
		CollisionShape = GetNode<CollisionShape3D>("CollisionShape3D");
	}

	public void OnClick()
	{
		_global.PickedObject = this;
		CollisionShape.Disabled = true;
	}
}
