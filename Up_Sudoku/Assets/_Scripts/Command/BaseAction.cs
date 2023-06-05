public abstract class BaseAction
{
	protected CellController cell;
	public abstract void Execute();

	public abstract void Undo();

	protected BaseAction(CellController cell)
	{
		this.cell = cell;
	}
    
}