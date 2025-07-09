using Godot;
using System;

namespace neighborhoodPizzeria.features.characters.player;

public partial class StaminaController : Node
{
	public float CurrentStamina = 100;
	public float MaxStamina = 100;

	private float _staminaRecoveryRate = 10;
	private float _staminaDepletionRate = 15;

	private MovementController _movementController;

	public override void _Ready()
	{
		var player = GetNode<CharacterBody3D>("/root/Node3D/Player");
		_movementController = player.GetNode<MovementController>("MovementController");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_movementController.IsSprinting)
		{
			CurrentStamina -= _staminaDepletionRate * (float)delta;

			if (CurrentStamina <= 0)
			{
				_movementController.IsSprinting = false;
			}
		}
		else if (CurrentStamina < MaxStamina)
		{
			CurrentStamina = float.Min(_staminaRecoveryRate * (float)delta + CurrentStamina, MaxStamina);
		}
	}
}
