using System;
using System.Collections.Generic;
using UnityEngine;
using XomracUtilities.Patterns;

namespace _Scripts.Grid
{

	public class CellHighlighter : ServiceComponent<CellController>
	{
		[SerializeField] private ThemePalette themePalette;

		private void OnEnable()
		{
			CellController.Clicked += HighLightCell;
			GameManager.GameResetted += OnGameReset;

		}

		private void OnDisable()
		{
			GameManager.GameResetted -= OnGameReset;
			CellController.Clicked -= HighLightCell;
		}

		private void HighLightCell(CellController selectedCell)
		{
			
			ResetColors();
			HighlightRowsColumnsSquares(selectedCell);
			HiglightSameValue(selectedCell);
			if (selectedCell == ServiceLocator)
			{
				ServiceLocator.SetBackgroundColor(themePalette.SelectedCellColor);
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
				ServiceLocator.SetBackgroundColor(themePalette.ColRowSquaresCellsColor);
			}
		}

		private void HiglightSameValue(CellController selectedCell)
		{
			if (!selectedCell.CurrentValue.HasValue) return;

			if (selectedCell.CurrentValue == ServiceLocator.CurrentValue)
			{
				ServiceLocator.SetBackgroundColor(themePalette.SameNumberColor);
			}
		}

		private void OnGameReset()
		{
			ResetColors();
		}
		

		private void ResetColors()
		{
			ServiceLocator.SetBackgroundColor(themePalette.NormalCellsColor);
		}

	}

}