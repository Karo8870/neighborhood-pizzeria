using Godot;
using System;
using Godot.Collections;

namespace neighborhoodPizzeria.features.props.pizza;

public partial class Pizza : Node3D
{
	private readonly Dictionary<string, PackedScene> toppings = new Dictionary<string, PackedScene>
	{
		// ["cheese"] = GD.Load<PackedScene>("res://ToppingCheese.tscn"),
		// ["pepperoni"] = GD.Load<PackedScene>("res://ToppingPepperoni.tscn"),
		// ["olive"] = GD.Load<PackedScene>("res://ToppingOlive.tscn")
	};

	private Node3D _ingredients;

	public override void _Ready()
	{
		_ingredients = GetNode<Node3D>("Ingredients");
	}

	private static Random _random = new Random();

	public static Vector3 GetRandomPointOnPizza(float radius)
	{
		double angle = _random.NextDouble() * 2 * Math.PI;
		float x = radius * (float)Math.Cos(angle);
		float y = radius * (float)Math.Sin(angle);
		return new Vector3(x, 0, y);
	}

	public void AddTopping(string toppingName)
	{
		for (int i = 0; i < 7; i++)
		{
			var slice = toppings[toppingName].Instantiate<MeshInstance3D>();
			slice.Position = GetRandomPointOnPizza(1);
			_ingredients.AddChild(slice);
		}
	}
}
