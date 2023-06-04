using System;
using _Scripts.Grid;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	[SerializeField] private int pointsForCompletedCell;

	
	[SerializeField] private int currentScore;

	public static event Action<int> ScoreChanged;

	private void OnEnable()
	{
		CellController.ValueInsterted += UpdateScore;
		GameManager.GameResetted += OnGameResetted;
	}

	private void OnDisable()
	{
		CellController.ValueInsterted -= UpdateScore;
		GameManager.GameResetted -= OnGameResetted;

	}

	private void OnGameResetted()
	{
		currentScore = 0;
	}

	private void UpdateScore(bool cellHasCorrectValue)
	{
		if (!cellHasCorrectValue) return;
		
		currentScore += pointsForCompletedCell;
		ScoreChanged?.Invoke(currentScore);
	}

}