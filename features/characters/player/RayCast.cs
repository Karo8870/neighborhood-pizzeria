using Godot;
using System.Linq;

namespace neighborhoodPizzeria;

public partial class RayCast : RayCast3D
{
    private Label _label;

    private Global _global;
    private Node3D _currentTarget;

    public override void _Ready()
    {
        _label = GetNode<Label>("Label");
        _global = GetNode<Global>("/root/Global");
    }

    private void CallOptionalMethod(string method, Node3D target)
    {
        if (target == null)
        {
            return;
        }

        if (target.HasMethod(method))
        {
            target.Call(method);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!_global.CanMove)
        {
            ClearHover();
            return;
        }

        Node3D newTarget = null;
        if (IsColliding())
        {
            GD.Print("Coliding");
            newTarget = GetCollider() as Node3D;
        }


        if (newTarget != _currentTarget)
        {
            if (_currentTarget != null && _currentTarget.HasMethod("OnHoverEnd"))
                _currentTarget.Call("OnHoverEnd");

            _currentTarget = newTarget;

            if (_currentTarget != null && _currentTarget.HasMethod("OnHoverStart"))
                _currentTarget.Call("OnHoverStart");

            UpdateLabel();
        }
    }

    private void ClearHover()
    {
        if (_currentTarget != null && _currentTarget.HasMethod("OnHoverEnd"))
            _currentTarget.Call("OnHoverEnd");
        _currentTarget = null;
        _label.Text = "";
    }

    private void UpdateLabel()
    {
        if (_currentTarget == null)
        {
            _label.Text = "";
            return;
        }

        var propNames = _currentTarget.GetPropertyList()
            .Select(p => p["name"].ToString());
        if (propNames.Contains("HintText"))
        {
            var txt = _currentTarget.Get("HintText").ToString();
            _label.Text = txt;
        }
        else
        {
            _label.Text = "";
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (_currentTarget == null)
        {
            return;
        }

        if (@event is InputEventMouseButton mb && mb.ButtonIndex == MouseButton.Left)
        {
            if (mb.Pressed)
            {
                CallOptionalMethod("OnClick", _currentTarget);
            }
            else
            {
                CallOptionalMethod("OnClickEnd", _currentTarget);
            }
        }
    }
}