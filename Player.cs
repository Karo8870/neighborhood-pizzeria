using Godot;


namespace horror;

public partial class Player : CharacterBody3D
{
	[Export] public float MoveSpeed = 6.0f;
	[Export] public float MouseSensitivity = 0.15f;
	[Export] public float JumpVelocity = 4f;
	[Export] public float Gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	[Export] public float MaxLookUp = 85.0f;
	[Export] public float MaxLookDown = -85.0f;

	private Node3D _head;
	private float _mouseLookVertical;
	private Vector3 _velocity = Vector3.Zero;

	public override void _Ready()
	{
		_head = GetNode<Node3D>(".");
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseMotion)
		{
			RotateY(Mathf.DegToRad(-mouseMotion.Relative.X * MouseSensitivity));

			_mouseLookVertical = Mathf.Clamp(_mouseLookVertical - mouseMotion.Relative.Y * MouseSensitivity,
				MaxLookDown,
				MaxLookUp);
			var headRot = _head.RotationDegrees;
			headRot.X = _mouseLookVertical;
			_head.RotationDegrees = headRot;
		}

		if (@event is InputEventKey { Pressed: true, Keycode: Key.Escape })
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		var inputDir = new Vector2(
			Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
			Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up")
		).Normalized();

		var direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

		_velocity.X = direction.X * MoveSpeed;
		_velocity.Z = direction.Z * MoveSpeed;

		if (!IsOnFloor())
		{
			_velocity.Y -= Gravity * (float)delta;
		}
		else
		{
			if (Input.IsActionJustPressed("ui_select"))
			{
				_velocity.Y = JumpVelocity;
			}

			else
			{
				_velocity.Y = 0;
			}
		}

		Velocity = _velocity;
		MoveAndSlide();
	}
}
