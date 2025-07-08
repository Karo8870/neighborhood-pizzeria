using Godot;

namespace neighborhoodPizzeria.features.characters.player;

public partial class BodyMovement : Node
{
    [Export] public float Speed = 5.0f;
    [Export] public float JumpVelocity = 4.5f;

    private CharacterBody3D _player;
    private Node3D _neck;
    private Global _global;

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

        if (dir != Vector3.Zero)
        {
            velocity.X = dir.X * Speed;
            velocity.Z = dir.Z * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(velocity.Z, 0, Speed);
        }

        _player.Velocity = velocity;
        _player.MoveAndSlide();
    }
}