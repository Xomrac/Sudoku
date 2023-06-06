using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XomracUtilities.Patterns;

namespace _Scripts.Grid
{

	public class CellHighlighter : ServiceComponent<CellController>
	{

		[SerializeField] private Image background;
		
		
		private void OnEnable()
		{
			CellController.Clicked += HighLightCell;
			GameManager.GameResetted += OnGameReset;
			CellController.CellUpdated += HighLightCell;
		}

		private void OnDisable()
		{
			CellController.Clicked -= HighLightCell;
			GameManager.GameResetted -= OnGameReset;
			CellController.CellUpdated -= HighLightCell;
		}

		public void ResetColors()
		{
			background.color = ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.normalCellsColor);
		}
		private void HighLightCell(CellController selectedCell)
		{
			ResetColors();
			HighlightRowsColumnsSquares(selectedCell);
			HiglightSameValue(selectedCell);
			if (selectedCell == ServiceLocator)
			{
				background.color = ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.selectedCellColor);
			}
		}

		private void HighlightRowsColumnsSquares(CellController selectedCell)
		{
			if (selectedCell == ServiceLocator) return;
			
			bool isInSameRow = ServiceLocator.node.CellRow == selectedCell.node.CellRow;
			bool isInSameColumn = ServiceLocator.node.CellColumn == selectedCell.node.CellColumn;
			bool isInSameSquare = ServiceLocator.node.CellSquare == selectedCell.node.CellSquare;

			
			if (isInSameColumn || isInSameRow || isInSameSquare)
			{
				background.color = ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.colRowSquareCellsColor);
			}
		}

		private void HiglightSameValue(CellController selectedCell)
		{
			if (!selectedCell.CurrentValue.HasValue) return;

			if (selectedCell.CurrentValue == ServiceLocator.CurrentValue)
			{
				background.color = ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.sameNumberCellsColor);
			}
		}

		private void OnGameReset()
		{
			ResetColors();
		}
		

		

	}

}