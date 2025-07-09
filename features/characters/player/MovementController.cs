using Godot;

namespace neighborhoodPizzeria.features.characters.player;

public partial class MovementController : Node
{
	[Export] public float Speed = 5.0f;
	[Export] public float RunningSpeed = 12.0f;
	[Export] public float JumpVelocity = 8f;

	private CharacterBody3D _player;
	private Node3D _neck;
	private Global _global;

	public bool IsSprinting = false;

	public override void _Ready()
	{
		_player = GetParent<CharacterBody3D>();
		_neck = _player.GetNode<Node3D>("Neck");
		_global = GetNode<Global>("/root/Global");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!_global.CanMove)
		{
			return;
		}

		if (Input.IsActionJustPressed("sprint"))
		{
			IsSprinting = true;
		}
		else if (Input.IsActionJustReleased("sprint"))
		{
			IsSprinting = false;
		}

		var velocity = _player.Velocity;

		if (!_player.IsOnFloor())
		{
			velocity += _player.GetGravity() * (float)delta;
		}


		if (Input.IsActionJustPressed("jump") && _player.IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}


		var inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");
		var basis = _neck.GlobalTransform.Basis;
		var forward = basis.Z;
		var right = basis.X;
		var dir = (right * inputDir.X + forward * inputDir.Y).Normalized();

		var currentSpeed = IsSprinting ? RunningSpeed : Speed;

		if (dir != Vector3.Zero)
		{
			velocity.X = dir.X * currentSpeed;
			velocity.Z = dir.Z * currentSpeed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(velocity.X, 0, currentSpeed);
			velocity.Z = Mathf.MoveToward(velocity.Z, 0, currentSpeed);
		}

		_player.Velocity = velocity;
		_player.MoveAndSlide();
	}
}
