using System;
using Godot;

namespace neighborhoodPizzeria.features.characters.player;

public partial class PickupController : Node
{
	[Export] public float PullPower = 10f;

	private Marker3D _hand;
	private Global _global;
	private CharacterBody3D _player;

	public override void _Ready()
	{
		_player = GetParent<CharacterBody3D>();
		_hand = _player.GetNode<Marker3D>("Neck/Camera3D/Hand");
		_global = GetNode<Global>("/root/Global");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!_global.CanMove)
			return;

		var picked = _global.PickedObject;
		if (picked == null || !IsInstanceValid(picked))
			return;

		// pull it toward the hand
		var objPos = picked.GlobalTransform.Origin;
		var handPos = _hand.GlobalTransform.Origin;
		try
		{
			var rb = picked as RigidBody3D;
			rb.SetLinearVelocity((handPos - objPos) * PullPower);
		}
		catch (NullReferenceException)
		{
			// ignore
		}

		// drop on escape
		if (Input.IsActionPressed("escape"))
		{
			picked.GetNode<CollisionShape3D>("CollisionShape3D").Disabled = false;
			_global.PickedObject = null;
		}
	}
}
