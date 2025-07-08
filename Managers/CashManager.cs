using Godot;
using System;

namespace neighborhoodPizzeria.Managers;

public partial class CashManager : Node
{
	[Signal]
	public delegate void MoneyChangedEventHandler(long newAmount);

	private long _amount = 0;

	public long Amount
	{
		get => _amount;
		set
		{
			if (_amount == value)
				return;
			_amount = value;
			EmitSignal(SignalName.MoneyChanged, _amount);
		}
	}

	public void Add(long value) => Amount += value;
	public void Subtract(long value) => Amount -= value;
}
