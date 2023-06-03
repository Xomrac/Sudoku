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
		}

		private void SelectCell(CellController cell)
		{
			currentSelectedCell = cell;
		}
	}

}