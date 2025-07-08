using System;
using Godot;

namespace neighborhoodPizzeria;

public partial class Player : CharacterBody3D
{
	private const float Speed = 5.0f;
	private const float JumpVelocity = 4.5f;

	private const float MouseSensitivity = 0.005f;

	private const float MinHeadRotation = -85;
	private const float MaxHeadRotation = 85;

	private Node3D _neck;
	private Camera3D _camera;
	private Marker3D _hand;

	private Global _global;

	private const float PullPower = 10;

	public bool is_sitting = false;


	public override void _Ready()
	{
		_neck = GetNode<Node3D>("Neck");
		_camera = GetNode<Camera3D>("Neck/Camera3D");
		_hand = GetNode<Marker3D>("Neck/Camera3D/Hand");
		_global = GetNode<Global>("/root/Global");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_global.PickedObject != null && IsInstanceValid(_global.PickedObject))
		{
			var objectTransform = _global.PickedObject.GlobalTransform.Origin;
			var handTransform = _hand.GlobalTransform.Origin;

			try
			{
				(_global.PickedObject as RigidBody3D).SetLinearVelocity((handTransform - objectTransform) * PullPower);
			}
			catch (NullReferenceException)
			{
			}

			if (Input.IsActionPressed("escape"))
			{
				_global.PickedObject.GetNode<CollisionShape3D>("CollisionShape3D").Disabled = false;
				_global.PickedObject = null;
			}
		}

		if (!_global.CanMove)
		{
			return;
		}

		Vector3 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");

		Basis neckBasis = _neck.GlobalTransform.Basis;
		Vector3 forward = neckBasis.Z;
		Vector3 right = neckBasis.X;

		Vector3 direction = (right * inputDir.X + forward * inputDir.Y).Normalized();

		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public override void _Input(InputEvent @event)
	{
		if (!_global.CanMove)
		{
			return;
		}

		if (@event is InputEventMouseMotion mouseMotion)
		{
			if (Input.MouseMode != Input.MouseModeEnum.Captured)
				return;

			_neck.RotateY(-mouseMotion.Relative.X * MouseSensitivity);

			float newX = _camera.Rotation.X - mouseMotion.Relative.Y * MouseSensitivity;
			newX = Mathf.Clamp(newX, Mathf.DegToRad(MinHeadRotation), Mathf.DegToRad(MaxHeadRotation));

			_camera.Rotation = new Vector3(newX, _camera.Rotation.Y, _camera.Rotation.Z);
		}
		else if (Input.IsActionPressed("escape"))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
		else
		{
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseMotion && @event.IsPressed())
		{
			var from = _camera.ProjectRayOrigin(mouseMotion.Position);
			var to = from + _camera.ProjectRayNormal(mouseMotion.Position) * 1000;

			var space_state = GetWorld3D().DirectSpaceState;
		}
	}
}
