
using Grid.Cells;

namespace Command
{
	public class InputNoteAction : BaseAction
	{

		#region Fields
		
		private readonly int valueToInput;

		#endregion

		#region Overrides
		
		public InputNoteAction(CellController cell, int valueToInput) : base(cell)
		{
			this.valueToInput = valueToInput;
		}
		
		public override void Execute()
		{
			cell.GetService<CellNotesController>()?.ToggleNote(valueToInput);
		}

		public override void Undo()
		{
			cell.GetService<CellNotesController>().ToggleNote(valueToInput);
		}
		
		#endregion
	}
}