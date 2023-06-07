using System;
using System.Collections.Generic;
using _Scripts.Grid;
using _Scripts.Grid.Themes;
using DG.Tweening;
using GameSystem;
using UnityEngine;
using UnityEngine.UI;
using XomracUtilities.Patterns;

namespace Grid.Cells
{

	public class CellHighlighter : ServiceComponent<CellController>
	{

		[SerializeField] private Image background;
		[SerializeField] private float colorTransitionTime=.2f;

		
		
		
		private void OnEnable()
		{
			CellController.CellClicked += OnCellClicked;
			CellController.CellUpdated += OnCellClicked;
			GameManager.NewGameStarted += OnNewGameStarted;

		}

		private void OnDisable()
		{
			CellController.CellClicked -= OnCellClicked;
			CellController.CellUpdated -= OnCellClicked;
			GameManager.NewGameStarted -= OnNewGameStarted;

		}
		
		
		
		public void ResetColors()
		{
			background.DOColor(ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.normalCellsColor), colorTransitionTime);
		}
		private void OnCellClicked(CellController selectedCell)
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
			
			bool isInSameRow = ServiceLocator.Node.CellRow == selectedCell.Node.CellRow;
			bool isInSameColumn = ServiceLocator.Node.CellColumn == selectedCell.Node.CellColumn;
			bool isInSameSquare = ServiceLocator.Node.CellSquare == selectedCell.Node.CellSquare;

			
			if (isInSameColumn || isInSameRow || isInSameSquare)
			{
				background.DOColor(ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.colRowSquareCellsColor),colorTransitionTime);
			}
		}

		private void HiglightSameValue(CellController selectedCell)
		{
			if (!selectedCell.GetService<CellInput>().CurrentValue.HasValue) return;

			if (selectedCell.GetService<CellInput>().CurrentValue == ServiceLocator.GetService<CellInput>().CurrentValue)
			{
				background.DOColor(ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.sameNumberCellsColor),colorTransitionTime);
			}
		}

		private void OnNewGameStarted(bool newGrid)
		{
			ResetColors();
		}
		

		

	}

}