using Godot;
using System;

// using neighborhoodPizzeria.Ingredients;
// using neighborhoodPizzeria.Orders;

namespace neighborhoodPizzeria;

public partial class StorageArea : Area3D
{
	// IngredientManager _ingredientManager;
	//
	// public override void _Ready()
	// {
	// 	BodyEntered += OnBodyEntered;
	// 	_ingredientManager = GetNode<IngredientManager>("/root/IngredientManager");
	// }
	//
	// private void OnBodyEntered(Node3D body)
	// {
	// 	if (body.IsInGroup("DeliveryBox"))
	// 	{
	// 		var parent = body.GetParent();
	//
	// 		_ingredientManager.Add(parent.GetMeta("ingredient").AsString(),
	// 			parent.GetMeta("quantity").AsInt32());
	// 		body.QueueFree();
	// 	}
	// }
}
