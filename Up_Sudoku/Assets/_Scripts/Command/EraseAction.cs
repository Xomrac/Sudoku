using System.Collections.Generic;
using Grid.Cells;

namespace Command
{
	public class EraseAction : BaseAction
	{

		#region Fields

		private readonly int? previousValue;
		private readonly List<int> activeNotes;

		#endregion

		#region Overrides
		
		public EraseAction(CellController cell, int? previousValue, List<int> notes) : base(cell)
		{
			this.previousValue = previousValue;
			activeNotes = notes;
		}
		
		public override void Execute()
		{
			cell.GetService<CellInput>().EraseValue();
		}

		public override void Undo()
		{
			cell.GetService<CellInput>()?.RestorePreviousValue(previousValue);
			cell.GetService<CellNotesController>()?.ActivatesNotes(activeNotes);
		}
		
		#endregion
	}
}