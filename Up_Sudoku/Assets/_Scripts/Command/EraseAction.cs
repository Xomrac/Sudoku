using System.Collections.Generic;

public class EraseAction : BaseAction
{
	public int? previousValue;
	public List<int> activeNotes;
	public override void Execute()
	{
		cell.RemoveValue();
	}

	public override void Undo()
	{
		cell.ReInsertPreviousValue(previousValue);
		cell.NotesController?.ActivatesNotes(activeNotes);
	}

	public EraseAction(CellController cell, int? previousValue, List<int> notes) : base(cell)
	{
		this.previousValue = previousValue;
		activeNotes = notes;
	}
}