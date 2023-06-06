using System;
using _Scripts.Grid;
using UnityEngine;

public class ErrorsManager : MonoBehaviour
{
	[SerializeField] private int currentErrors;
	public static event Action GameLost;
	public static event Action<int> ErrorsChanged;

	private void OnEnable()
	{
		CellController.ValueInsterted += CheckForError;
		GameManager.GameResetted += OnGameResetted;
		GameManager.GameRestarted += OnGameResetted;
	}

	private void OnDisable()
	{
		CellController.ValueInsterted -= CheckForError;
		GameManager.GameResetted -= OnGameResetted;
		GameManager.GameRestarted -= OnGameResetted;


	}

	private void OnGameResetted()
	{
		currentErrors = 0;
	}

	private void CheckForError(bool cellHasCorrectValue)
	{
		if (cellHasCorrectValue) return;
		
		currentErrors++;
		ErrorsChanged?.Invoke(currentErrors);
		if (currentErrors>=GameManager.Instance.gameSettings.MaxErrors)
		{
			GameLost?.Invoke();
		}
	}
}