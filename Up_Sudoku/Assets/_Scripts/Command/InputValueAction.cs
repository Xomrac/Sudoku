public class InputValueAction : BaseAction
{
	private int valueToInput;
	private int? previousValue;

	public override void Execute()
	{
		cell.CurrentValue = valueToInput;
	}

	public override void Undo()
	{
		cell.ReInsertPreviousValue(previousValue);
	}

	public InputValueAction(CellController cell, int? currentValue, int valueToInput) : base(cell)
	{
		this.valueToInput = valueToInput;
		previousValue = currentValue;
	}
}
