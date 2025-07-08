using Godot;

namespace neighborhoodPizzeria.features.characters.player;

public partial class CameraController : Node
{
	[Export] public float MouseSensitivity = 0.005f;
	[Export] public float MinHeadRotation = -85f;
	[Export] public float MaxHeadRotation = 85f;

	private CharacterBody3D _player;
	private Node3D _neck;
	private Camera3D _camera;
	private Global _global;

	public override void _Ready()
	{
		_player = GetParent<CharacterBody3D>();
		_neck = _player.GetNode<Node3D>("Neck");
		_camera = _neck.GetNode<Camera3D>("Camera3D");
		_global = GetNode<Global>("/root/Global");
	}

	public override void _Input(InputEvent @event)
	{
		if (!_global.CanMove)
			return;

		if (@event is InputEventMouseMotion mm &&
			Input.MouseMode == Input.MouseModeEnum.Captured)
		{
			// yaw
			_neck.RotateY(-mm.Relative.X * MouseSensitivity);

			// pitch
			float newX = _camera.Rotation.X - mm.Relative.Y * MouseSensitivity;
			newX = Mathf.Clamp(
				newX,
				Mathf.DegToRad(MinHeadRotation),
				Mathf.DegToRad(MaxHeadRotation)
			);
			_camera.Rotation = new Vector3(
				newX,
				_camera.Rotation.Y,
				_camera.Rotation.Z
			);
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
}
