using System;
using _Scripts.Grid;
using UnityEngine;

public class ErrorsManager : MonoBehaviour
{
	[SerializeField] private int currentErrors;
	public static event Action<int> ErrorMade;

	private void OnEnable()
	{
		CellController.ValueInsterted += CheckForError;
		GameManager.GameResetted += OnGameResetted;
	}

	private void OnDisable()
	{
		CellController.ValueInsterted -= CheckForError;
		GameManager.GameResetted -= OnGameResetted;

	}

	private void OnGameResetted()
	{
		currentErrors = 0;
	}

	private void CheckForError(bool cellHasCorrectValue)
	{
		if (cellHasCorrectValue) return;
		
		currentErrors++;
		ErrorMade?.Invoke(currentErrors);
	}
}