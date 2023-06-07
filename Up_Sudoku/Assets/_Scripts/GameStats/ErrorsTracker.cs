using System;
using GameSystem;
using Grid.Cells;
using XomracUtilities.Patterns;

namespace GameStats
{

	public class ErrorsTracker : ServiceComponent<GameStatsManager>
	{

		#region Fields

		private int currentErrors;
		public int CurrentErrors => currentErrors;

		public static event Action GameLost;
		public static event Action<int> ErrorsChanged;

		#endregion

		#region LifeCycle

		private void OnEnable()
		{
			CellInput.ValueInsterted += OnValueInserted;

			GameManager.NewGameStarted += OnNewGameStarted;
		}

		private void OnDisable()
		{
			CellInput.ValueInsterted -= OnValueInserted;

			GameManager.NewGameStarted -= OnNewGameStarted;
		}

		#endregion

		#region Callbacks

		private void OnNewGameStarted(bool newGrid)
		{
			currentErrors = 0;
		}

		private void OnValueInserted(bool cellHasCorrectValue)
		{
			if (cellHasCorrectValue) return;

			currentErrors++;
			ErrorsChanged?.Invoke(currentErrors);
			if (currentErrors >= GameManager.Instance.GameRules.MaxErrors)
			{
				GameLost?.Invoke();
			}
		}

		#endregion

	}

}