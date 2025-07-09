using Godot;
using System;
using neighborhoodPizzeria.features.characters.player;

namespace neighborhoodPizzeria.features.ui.stamina_bar;

public partial class StaminaBar : ProgressBar
{
	private StaminaController _staminaController;

	public override void _Ready()
	{
		var player = GetNode<CharacterBody3D>("/root/Node3D/Player");
		_staminaController = player.GetNode<StaminaController>("StaminaController");
	}

	public override void _Process(double delta)
	{
		Value = _staminaController.CurrentStamina;
	}
}
