using Grid.Cells;

namespace Command
{
	public class InputValueAction : BaseAction
	{
		#region Fields

		private readonly int valueToInput;
		private readonly int? previousValue;

		#endregion

		#region Overrides

		public InputValueAction(CellController cell, int? currentValue, int valueToInput) : base(cell)
		{
			this.valueToInput = valueToInput;
			previousValue = currentValue;
		}

		public override void Execute()
		{
			cell.InputValue(valueToInput);
		}

		public override void Undo()
		{
			cell.RestorePreviousValue(previousValue);
		}
		
		#endregion
	}
}
