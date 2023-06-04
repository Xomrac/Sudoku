using System;
using UnityEngine;
using XomracUtilities.Patterns;

namespace _Scripts.Grid
{

	public class GameManager : Singleton<GameManager>
	{
		public GameSettings gameSettings;
		public CellController currentSelectedCell;
		public static event Action GameResetted;

		public bool IsZenMode => gameSettings.GameMode == GameMode.Zen;

		private void OnEnable()
		{
			CellController.Clicked += SelectCell;
			ErrorsManager.ErrorMade += CheckForGameOver;
		}
		private void OnDisable()
		{
			CellController.Clicked -= SelectCell;
			ErrorsManager.ErrorMade -= CheckForGameOver;

		}


		public void ResetGame()
		{
			currentSelectedCell = null;
			GameResetted?.Invoke();
		}
		

		private void Erase()
		{
			if (currentSelectedCell==null || currentSelectedCell.CurrentlyCompleted) return;
			currentSelectedCell.RemoveValue();
		}

	

		private void SelectCell(CellController cell)
		{
			currentSelectedCell = cell;
		}

		private void CheckForGameOver(int amountOfErrors)
		{
			if (amountOfErrors >= gameSettings.MaxErrors)
			{
				Debug.Log("GAME OVER!");
			}
		}

		public void CheckForVictory()
		{
			
		}
	}

}