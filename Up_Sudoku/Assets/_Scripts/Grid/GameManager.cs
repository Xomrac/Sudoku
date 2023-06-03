using System;
using UnityEngine;
using XomracUtilities.Patterns;

namespace _Scripts.Grid
{

	public class GameManager : Singleton<GameManager>
	{
		public CellController currentSelectedCell;

		private void OnEnable()
		{
			CellController.Clicked += SelectCell;
			NumberSelector.Selected += PlaceValue;
		}
		private void OnDisable()
		{
			CellController.Clicked -= SelectCell;
			NumberSelector.Selected -= PlaceValue;
		}
		private void PlaceValue(int value)
		{
			if (currentSelectedCell==null || currentSelectedCell.CurrentlyCompleted) return;
			currentSelectedCell.CurrentValue = value;
		}

		private void Erase()
		{
			if (currentSelectedCell==null || currentSelectedCell.CurrentlyCompleted) return;
			currentSelectedCell.CurrentValue = null;
		}

	

		private void SelectCell(CellController cell)
		{
			currentSelectedCell = cell;
		}
	}

}