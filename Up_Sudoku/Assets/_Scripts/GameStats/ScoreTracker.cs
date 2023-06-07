using System;
using GameStats;
using GameSystem;
using Grid.Cells;
using UnityEngine;
using XomracUtilities.Patterns;

public class ScoreTracker : ServiceComponent<GameStatsManager>
{

	#region Fields

	[SerializeField] private int pointsForCompletedCell;
	
	private int currentScore;
	public static event Action<int> ScoreChanged;

	#endregion

	#region LifeCycle

	private void OnEnable()
	{
		CellInput.ValueInsterted += OnValueInsterd;
		GameManager.NewGameStarted += OnNewGameStarted;
	}

	private void OnDisable()
	{
		CellInput.ValueInsterted -= OnValueInsterd;
		GameManager.NewGameStarted -= OnNewGameStarted;
	}

	#endregion

	#region Callbacks

	private void OnNewGameStarted(bool newGame)
	{
		currentScore = 0;
	}

	private void OnValueInsterd(bool cellHasCorrectValue)
	{
		if (!cellHasCorrectValue) return;
		
		currentScore += pointsForCompletedCell;
		ScoreChanged?.Invoke(currentScore);
	}

	#endregion
	

}