using Godot;
using System;

namespace neighborhoodPizzeria;

public partial class Computer : StaticBody3D
{
	private CanvasLayer _menu;
	private Global _global;

	public override void _Ready()
	{
		_menu = GetNode<CanvasLayer>("%Menu");
		_global = GetNode<Global>("/root/Global");
		
	}

	public void OnClick()
	{
		_menu.Visible = true;
		Input.MouseMode = Input.MouseModeEnum.Visible;
		_global.CanMove = false;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("escape"))
		{
			_menu.Visible = false;
			Input.MouseMode = Input.MouseModeEnum.Captured;
			_global.CanMove = true;
		}
	}
}
