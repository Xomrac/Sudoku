using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using XomracUtilities.Patterns;

namespace _Scripts.Grid
{

	public class CellHighlighter : ServiceComponent<CellController>
	{

		[SerializeField] private Image background;
		[SerializeField] private float colorTransitionTime=.2f;

		
		
		
		private void OnEnable()
		{
			CellController.Clicked += HighLightCell;
			GameManager.GameResetted += OnGameReset;
			CellController.CellUpdated += HighLightCell;
			GameManager.GameRestarted += OnGameReset;

		}

		private void OnDisable()
		{
			CellController.Clicked -= HighLightCell;
			GameManager.GameResetted -= OnGameReset;
			CellController.CellUpdated -= HighLightCell;
			GameManager.GameRestarted -= OnGameReset;

		}
		
		
		
		public void ResetColors()
		{
			background.DOColor(ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.normalCellsColor), colorTransitionTime);
		}
		private void HighLightCell(CellController selectedCell)
		{
			ResetColors();
			HighlightRowsColumnsSquares(selectedCell);
			HiglightSameValue(selectedCell);
			if (selectedCell == ServiceLocator)
			{
				background.DOColor(ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.selectedCellColor),colorTransitionTime);
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
				background.DOColor(ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.colRowSquareCellsColor),colorTransitionTime);
			}
		}

		private void HiglightSameValue(CellController selectedCell)
		{
			if (!selectedCell.CurrentValue.HasValue) return;

			if (selectedCell.CurrentValue == ServiceLocator.CurrentValue)
			{
				background.DOColor(ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.sameNumberCellsColor),colorTransitionTime);
			}
		}

		private void OnGameReset()
		{
			ResetColors();
		}
		

		

	}

}