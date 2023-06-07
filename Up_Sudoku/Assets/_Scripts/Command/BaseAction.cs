using Grid.Cells;

namespace Command
{
	public abstract class BaseAction
	{
		#region Fields

		protected CellController cell;

		#endregion

		#region Methods

		protected BaseAction(CellController cell)
		{
			this.cell = cell;
		}

		public abstract void Execute();

		public abstract void Undo();

		#endregion
	}
}