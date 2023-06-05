public class InputNoteAction : BaseAction
{
	private int valueToInput;
	public override void Execute()
	{
		cell.NotesController.ToggleNote(valueToInput);
	}

	public override void Undo()
	{
		cell.NotesController.ToggleNote(valueToInput);
	}

	public InputNoteAction(CellController cell, int valueToInput) : base(cell)
	{
		this.valueToInput = valueToInput;
	}
}